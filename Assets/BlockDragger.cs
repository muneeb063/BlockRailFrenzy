using UnityEngine;
using Watermelon;
using DG.Tweening;
using Tween = DG.Tweening.Tween;
using Ease = DG.Tweening.Ease;

public enum BlockColorType
{
    Blue, Red, Yellow, Green, Pink, Purple, Any
}

public class BlockDragger : MonoBehaviour
{
    private Camera mainCam;
    private bool isDragging = false;
    private Vector3 offset;
    private float yOffset = 0.5f;
    private Vector3 originalPosition;
    public BlockColorType blockColor;
    private GridCellColor[] lastOccupiedCells;
    private Tween currentTween;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        HandleDrag();
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.IsChildOf(transform))
                {
                    if (IsOverlappingOtherBlocks())
                    {
                        Debug.Log("Dragging prevented: block is overlapping another block.");
                        return;
                    }

                    isDragging = true;
                    originalPosition = transform.position;

                    Plane plane = new Plane(Vector3.up, transform.position);
                    if (plane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPointOnPlane = ray.GetPoint(enter);
                        offset = transform.position - hitPointOnPlane;
                    }

                    currentTween?.Kill();
                }
            }
        }

        if (isDragging)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPointOnPlane = ray.GetPoint(enter);
                Vector3 targetPos = hitPointOnPlane + offset;
                targetPos.y = yOffset;

                float maxMoveDistance = 2.5f;
                float distance = Vector3.Distance(transform.position, targetPos);

                if (distance <= maxMoveDistance)
                {
                    // Try full move
                    if (!WouldOverlapAtPosition(targetPos))
                    {
                        currentTween?.Kill();
                        currentTween = transform.DOMove(targetPos, 0.12f).SetEase(Ease.OutQuad);
                    }
                    else
                    {
                        // Try a soft nudge towards finger
                        Vector3 softTarget = Vector3.Lerp(transform.position, targetPos, 0.05f);//0.15 tha
                        softTarget.y = yOffset;

                        if (!WouldOverlapAtPosition(softTarget))
                        {
                            currentTween?.Kill();
                            currentTween = transform.DOMove(softTarget, 0.12f).SetEase(Ease.OutSine);
                        }
                        // Else: block stays in place (prevent jerky jumps)
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                currentTween?.Kill();
                SnapToGrid();
            }
        }
    }


    void SnapToGrid()
    {
        Transform anchor = transform.GetChild(0);
        Vector3 anchorWorld = anchor.position;

        Vector2Int gridPos = GridManager.Instance.GetGridFromWorld(anchorWorld);
        Vector3 snappedWorld = GridManager.Instance.GetWorldFromGrid(gridPos.x, gridPos.y);

        Vector3 delta = snappedWorld - anchorWorld;
        Vector3 finalPos = transform.position + delta;
        finalPos.y = yOffset;

        transform.position = finalPos;

        bool isOverlapping = IsOverlappingOtherBlocks();

        if (isOverlapping || !IsAllChildrenInsideGridTriggers() || !IsBlockOverCorrectColorCells())
        {
            transform.DOMove(originalPosition, 0.2f).SetEase(Ease.InOutQuad);
            ClearLastOccupiedCells();
            return;
        }

        ClearLastOccupiedCells();
        RegisterOccupiedCells();
    }

    bool IsOverlappingOtherBlocks()
    {
        foreach (Transform child in transform)
        {
            Vector3 center = child.position;
            Vector3 halfExtents = Vector3.one * 0.45f;

            Collider[] hits = Physics.OverlapBox(center, halfExtents, Quaternion.identity);
            foreach (Collider hit in hits)
            {
                if (!hit.transform.IsChildOf(transform) && hit.CompareTag("BlockCube"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool WouldOverlapAtPosition(Vector3 proposedPosition)
    {
        Vector3 originalPos = transform.position;
        transform.position = proposedPosition;
        bool isOverlapping = IsOverlappingOtherBlocks();
        transform.position = originalPos;
        return isOverlapping;
    }

    public bool IsAllChildrenInsideGridTriggers()
    {
        foreach (Transform child in transform)
        {
            Vector3 center = child.position;
            Vector3 halfExtents = Vector3.one * 0.45f;
            Collider[] hits = Physics.OverlapBox(center, halfExtents, Quaternion.identity);

            bool foundGridCell = false;

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("GridCell"))
                {
                    foundGridCell = true;
                    break;
                }
            }

            if (!foundGridCell)
            {
                Debug.LogWarning("Child not on grid: " + child.name + " at " + child.position);
                return false;
            }
        }

        return true;
    }

    bool IsBlockOverCorrectColorCells()
    {
        foreach (Transform child in transform)
        {
            Vector3 checkPosition = child.position;
            Collider[] colliders = Physics.OverlapSphere(checkPosition, 1f);

            bool matchedColor = false;

            foreach (Collider col in colliders)
            {
                GridCellColor cell = col.GetComponent<GridCellColor>();
                if (cell != null)
                {
                    if (cell.allowedColor == BlockColorType.Any || cell.allowedColor == blockColor)
                    {
                        matchedColor = true;
                        break;
                    }
                }
            }

            if (!matchedColor)
            {
                Debug.LogWarning($"{child.name} at {checkPosition} is not over a valid color grid cell.");
                return false;
            }
        }

        return true;
    }

    void ClearLastOccupiedCells()
    {
        if (lastOccupiedCells == null) return;

        foreach (GridCellColor cell in lastOccupiedCells)
        {
            if (cell != null)
                cell.isOccupied = false;
        }

        lastOccupiedCells = null;

        foreach (Transform child in transform)
        {
            Collider[] hits = Physics.OverlapSphere(child.position, 0.4f);
            foreach (Collider hit in hits)
            {
                BusBehavior bus = hit.GetComponentInParent<BusBehavior>();
                if (bus != null)
                {
                    transform.SetParent(customManagerScript.instance.LevelArtData.transform);
                    return;
                }
            }
        }
    }

    void RegisterOccupiedCells()
    {
        var newList = new System.Collections.Generic.List<GridCellColor>();

        foreach (Transform child in transform)
        {
            Collider[] hits = Physics.OverlapSphere(child.position, 0.5f);
            foreach (Collider col in hits)
            {
                GridCellColor cell = col.GetComponent<GridCellColor>();
                if (cell != null && !newList.Contains(cell))
                {
                    cell.isOccupied = true;
                    newList.Add(cell);
                }
            }
        }

        lastOccupiedCells = newList.ToArray();

        foreach (Transform child in transform)
        {
            Collider[] hits = Physics.OverlapSphere(child.position, 0.4f);
            foreach (Collider hit in hits)
            {
                BusBehavior bus = hit.GetComponentInParent<BusBehavior>();
                if (bus != null)
                {
                    transform.SetParent(bus.transform);
                    return;
                }
            }
        }
    }
}
