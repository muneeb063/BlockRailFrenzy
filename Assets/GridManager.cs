using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int rows = 8;
    public int columns = 6;
    public GameObject cellPrefab;
    public Transform gridParent;
    public float spacing = 0.1f;

    public float cellSize = 1f;
    public Vector3 originPosition = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int z = 0; z < rows; z++)
        {
            for (int x = 0; x < columns; x++)
            {
                float posX = originPosition.x + x * (cellSize + spacing);
                float posZ = originPosition.z + z * (cellSize + spacing);
                Vector3 pos = new Vector3(posX, originPosition.y, posZ);
                Instantiate(cellPrefab, pos, Quaternion.identity, gridParent);
            }
        }
    }

    public Vector2Int GetGridFromWorld(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt((worldPos.x - originPosition.x) / (cellSize + spacing));
        int z = Mathf.RoundToInt((worldPos.z - originPosition.z) / (cellSize + spacing));
        return new Vector2Int(x, z);
    }

    public Vector3 GetWorldFromGrid(int x, int z)
    {
        float posX = originPosition.x + x * (cellSize + spacing);
        float posZ = originPosition.z + z * (cellSize + spacing);
        return new Vector3(posX, originPosition.y, posZ);
    }
   
}

