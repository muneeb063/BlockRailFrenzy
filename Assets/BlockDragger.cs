using UnityEngine;
using Watermelon;
public enum BlockColorType
{
    Blue,
    Red,
    Yellow,
    Green,
    Pink,
    Purple,
    Any
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
                    isDragging = true;
                    originalPosition = transform.position; // store current position

                    // Save offset from block center to hit point
                    Plane plane = new Plane(Vector3.up, transform.position);
                    if (plane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPointOnPlane = ray.GetPoint(enter);
                        offset = transform.position - hitPointOnPlane;
                    }
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
                transform.position = targetPos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                SnapToGrid();
               
            }
        }
    }

    void SnapToGrid()
    {
        // Use first child as anchor
        Transform anchor = transform.GetChild(0);
        Vector3 anchorWorld = anchor.position;

        // Convert world position to grid coords
        Vector2Int gridPos = GridManager.Instance.GetGridFromWorld(anchorWorld);
        Vector3 snappedWorld = GridManager.Instance.GetWorldFromGrid(gridPos.x, gridPos.y);

        // Offset the parent block so the anchor aligns
        Vector3 delta = snappedWorld - anchorWorld;
        transform.position += delta;

        // Lock Y position
        Vector3 fixedPos = transform.position;
        fixedPos.y = yOffset;
        transform.position = fixedPos;

        // Check for overlapping
        bool isOverlapping = false;

        foreach (Transform child in transform)
        {
            Vector3 pos = child.position;
            Vector3 halfExtents = child.localScale / 2f * 0.9f;

            Collider[] hits = Physics.OverlapBox(pos, halfExtents, child.rotation);
            foreach (Collider hit in hits)
            {
                if (!hit.transform.IsChildOf(transform) && hit.CompareTag("BlockCube"))
                {
                    isOverlapping = true;
                    break;
                }
            }

            if (isOverlapping) break;
        }

        /*   // Handle invalid placement
           if (isOverlapping || !IsAllChildrenInsideGridTriggers() || !IsBlockOverCorrectColorCells())
           {
               transform.position = originalPosition;
               return;
           }

           //  Mark occupied grid cells
           foreach (Transform child in transform)
           {
               Collider[] hits = Physics.OverlapSphere(child.position, 0.5f);
               foreach (Collider col in hits)
               {
                   GridCellColor cell = col.GetComponent<GridCellColor>();
                   if (cell != null)
                   {
                       cell.isOccupied = true;
                   }
               }
           }*/
        if (isOverlapping || !IsAllChildrenInsideGridTriggers() || !IsBlockOverCorrectColorCells())
        {
            transform.position = originalPosition;
            ClearLastOccupiedCells(); // Unmark previous cells
            return;
        }

        //  Valid placement
        ClearLastOccupiedCells();     // First clear old
        RegisterOccupiedCells();      // Then mark new
    }


    public bool IsAllChildrenInsideGridTriggers()
    {
        foreach (Transform child in transform)
        {
            Vector3 center = child.position;
            Vector3 halfExtents = Vector3.one * 0.45f; // fixed size instead of relying on scale
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
                Debug.LogWarning(" Child not on grid: " + child.name + " at " + child.position);
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
        /// Remove Block as Child
        foreach (Transform child in transform)
        {
            Collider[] hits = Physics.OverlapSphere(child.position, 0.4f);
            foreach (Collider hit in hits)
            {
                BusBehavior bus = hit.GetComponentInParent<BusBehavior>();
                if (bus != null)
                {
                    transform.SetParent(customManagerScript.instance.LevelArtData.transform); // Re-parent to Level
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
        /// Make Block Child
        foreach (Transform child in transform)
        {
            Collider[] hits = Physics.OverlapSphere(child.position, 0.4f);
            foreach (Collider hit in hits)
            {
                BusBehavior bus = hit.GetComponentInParent<BusBehavior>();
                if (bus != null)
                {
                    transform.SetParent(bus.transform); // Re-parent to bus
                    return;
                }
            }
        }
    }
}
