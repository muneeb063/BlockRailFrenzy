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
    private Vector3 currentTargetPos;
    private Plane dragPlane;

    public BlockColorType blockColor;
    private GridCellColor[] lastOccupiedCells;
    private Tween currentTween;

    [SerializeField] private float dragFollowSpeed = 20f;
    [SerializeField] private float moveCheckStep = 0.05f;

    void Start()
    {
        mainCam = Camera.main;
        AutoSnapToGrid();
    }
    void AutoSnapToGrid()
    {
        Transform anchor = transform.GetChild(0);
        Vector3 anchorWorld = anchor.position;

        Vector2Int gridPos = GridManager.Instance.GetGridFromWorld(anchorWorld);
        Vector3 snappedWorld = GridManager.Instance.GetWorldFromGrid(gridPos.x, gridPos.y);

        Vector3 delta = snappedWorld - anchorWorld;
        Vector3 finalPos = transform.position + delta;
        finalPos.y = yOffset;

        transform.position = finalPos;

        // Register occupied cells so grid knows this block is placed
        RegisterOccupiedCells();
    }
    void Update()
    {
#if UNITY_EDITOR
        HandleMouseDrag(); // for editor testing
#else
        HandleTouchDrag(); // for mobile
#endif

        if (isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, currentTargetPos, Time.deltaTime * dragFollowSpeed);
        }
    }

    void HandleTouchDrag()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.IsChildOf(transform))
                {
                    if (IsOverlappingOtherBlocks()) return;

                    isDragging = true;
                    originalPosition = transform.position;

                    dragPlane = new Plane(Vector3.up, transform.position);

                    if (dragPlane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        offset = transform.position - hitPoint;
                        currentTargetPos = transform.position; // initialize target pos to current
                    }

                    currentTween?.Kill();
                }
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                if (!isDragging) return;

                if (dragPlane.Raycast(ray, out float dragEnter))
                {
                    Vector3 hitPoint = ray.GetPoint(dragEnter);
                    Vector3 targetPos = hitPoint + offset;
                    targetPos.y = yOffset;

                    Vector3 finalPos = transform.position;

                    // Walk toward targetPos but stop if overlap occurs
                    for (float t = 0f; t <= 1f; t += 0.05f)
                    {
                        Vector3 step = Vector3.Lerp(transform.position, targetPos, t);
                        step.y = yOffset;

                        if (!WouldOverlapAtPosition(step))
                            finalPos = step;
                        else
                            break;
                    }

                    currentTargetPos = finalPos;
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (!isDragging) return;

                isDragging = false;
                SnapToGrid();
                break;
        }
    }


    void HandleMouseDrag() // For PC testing
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.IsChildOf(transform))
            {
                if (IsOverlappingOtherBlocks()) return;

                isDragging = true;
                originalPosition = transform.position;
                dragPlane = new Plane(Vector3.up, transform.position);

                if (dragPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    offset = transform.position - hitPoint;
                }

                currentTween?.Kill();
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 targetPos = hitPoint + offset;
                targetPos.y = yOffset;

                Vector3 direction = targetPos - transform.position;
                Vector3 candidatePos = transform.position;

                for (float t = 0f; t <= 1f; t += moveCheckStep)
                {
                    Vector3 stepPos = Vector3.Lerp(transform.position, targetPos, t);
                    stepPos.y = yOffset;

                    if (!WouldOverlapAtPosition(stepPos))
                        candidatePos = stepPos;
                    else
                        break;
                }

                currentTargetPos = candidatePos;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            SnapToGrid();
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

        if (IsOverlappingOtherBlocks() || !IsAllChildrenInsideGridTriggers() || !IsBlockOverCorrectColorCells())
        {
            currentTween = transform.DOMove(originalPosition, 0.2f).SetEase(Ease.InOutQuad);
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
                    return true;
            }
        }
        return false;
    }

    bool WouldOverlapAtPosition(Vector3 proposedPosition)
    {
        Vector3 offsetToTest = proposedPosition - transform.position;

        foreach (Transform child in transform)
        {
            Vector3 testPos = child.position + offsetToTest;
            Vector3 halfExtents = Vector3.one * 0.45f;

            Collider[] hits = Physics.OverlapBox(testPos, halfExtents, Quaternion.identity);
            foreach (Collider hit in hits)
            {
                if (!hit.transform.IsChildOf(transform) && hit.CompareTag("BlockCube"))
                    return true;
            }
        }
        return false;
    }

    bool IsAllChildrenInsideGridTriggers()
    {
        foreach (Transform child in transform)
        {
            Collider[] hits = Physics.OverlapBox(child.position, Vector3.one * 0.45f, Quaternion.identity);
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
                Debug.LogWarning("Child not on grid: " + child.name);
                return false;
            }
        }
        return true;
    }

    bool IsBlockOverCorrectColorCells()
    {
        foreach (Transform child in transform)
        {
            Collider[] colliders = Physics.OverlapSphere(child.position, 1f);
            bool matchedColor = false;

            foreach (Collider col in colliders)
            {
                GridCellColor cell = col.GetComponent<GridCellColor>();
                if (cell && (cell.allowedColor == BlockColorType.Any || cell.allowedColor == blockColor))
                {
                    matchedColor = true;
                    break;
                }
            }

            if (!matchedColor)
            {
                Debug.LogWarning($"{child.name} is not over a correct color grid cell.");
                return false;
            }
        }

        return true;
    }

    void ClearLastOccupiedCells()
    {
        if (lastOccupiedCells == null) return;

        foreach (GridCellColor cell in lastOccupiedCells)
            if (cell != null) cell.isOccupied = false;

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
