using UnityEngine;
using System.Collections.Generic;
using Facebook.Unity.Example;

//[CreateAssetMenu(menuName = "Block Shape")]
[CreateAssetMenu(menuName = "BlocksData/Block Shape")]
public class BlockShapeSO : ScriptableObject
{
    public string shapeName;
    public Color shapeColor;
    public List<Vector2Int> cells; // positions in shape
}