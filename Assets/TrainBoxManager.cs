using UnityEngine;
using Watermelon;

public class TrainBoxManager : MonoBehaviour
{
    public GridCellColor[] gridCells;

    private bool hasDeparted = false;

    void Update()
    {
        if (!hasDeparted && AreAllCellsOccupied())
        {
            hasDeparted = true;
            Debug.Log($"{gameObject.name} is FULL. Train moving!");
            MoveTrain(); // Your animation or transition here
        }
    }

    bool AreAllCellsOccupied()
    {
        foreach (var cell in gridCells)
        {
            if (!cell.isOccupied)
                return false;
        }
        return true;
    }

    void MoveTrain()
    {
        // Animate, destroy, slide, etc.
        // Debug.Log($" {gameObject.name} departing!");
        // Example: transform.Translate(Vector3.right * 10f);
        gameObject.transform.parent.gameObject.GetComponent<BusBehavior>().MoveToExit();
    }
}
