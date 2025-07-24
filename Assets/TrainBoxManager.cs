using UnityEngine;
using Watermelon;

public class TrainBoxManager : MonoBehaviour
{
    public GridCellColor[] gridCells;
    private bool hasDeparted = false;

    // Event to notify LevelManager
    public System.Action<TrainBoxManager> OnTrainDeparted;

    void Update()
    {
        if (!hasDeparted && AreAllCellsOccupied())
        {
            hasDeparted = true;
            Debug.Log($"{gameObject.name} is FULL. Train moving!");
            MoveTrain();
            OnTrainDeparted?.Invoke(this);  // Notify LevelManager
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
        customManagerScript.instance.TrainFillCount++;
        if (customManagerScript.instance.TrainFillCount == customManagerScript.instance.TrainCount)
        {
            Debug.Log("LEVEL COMPLETE!");
            GameController.WinGame();
            customManagerScript.instance.TrainFillCount = 0;
            gameObject.transform.parent.gameObject.GetComponent<BusBehavior>().MoveToExit();
        }
        else
        {
            gameObject.transform.parent.gameObject.GetComponent<BusBehavior>().MoveToExit();
        }
    }
}
