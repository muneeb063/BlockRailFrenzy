using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuilder : MonoBehaviour
{
    public BlockShapeSO blockData;
    public GameObject cubePrefab;

    private void Start()
    {
        BuildBlock();
    }
    public void BuildBlock()
    {
        /*  foreach (Vector2Int pos in blockData.cells)
          {
              GameObject part = Instantiate(cubePrefab, transform);
              part.transform.localPosition = new Vector3(pos.x, 0, pos.y);
              part.GetComponent<Renderer>().material.color = blockData.shapeColor;
          }*/
        foreach (Vector2Int pos in blockData.cells)
        {
            GameObject part = Instantiate(cubePrefab, transform);
            part.transform.localPosition = new Vector3(pos.x, 0f, pos.y);
            part.GetComponent<Renderer>().material.color = blockData.shapeColor;

            // Add collider if not already
            if (!part.TryGetComponent(out BoxCollider col))
            {
                part.AddComponent<BoxCollider>();
            }
        }
    }
}
