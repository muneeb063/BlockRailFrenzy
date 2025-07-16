using UnityEngine;
using Watermelon;
using Watermelon.BusStop;

public class RaycastTrain : MonoBehaviour
{
    // Distance for the raycast
    public float rayDistance = 10f;
    public bool shouldDetectTrain = false;
    public bool ShouldDetectDoor = false;
    void Update()
    {
        // Emit the ray
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.red);
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // Check if the hit object is on the "Train" layer
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Door") && ShouldDetectDoor == true)
            {
                hit.collider.transform.parent.GetComponent<BusBehavior>().PlayDoorAnim();
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("TrainLock") && shouldDetectTrain == true)
            {
                // Get the parent of the raycast object

                Transform raycastParent = transform.parent;

                if (raycastParent != null)
                {
                    // Set the train object as a child of the raycast object's parent
                    hit.collider.transform.parent.SetParent(raycastParent);
                    //Debug.Log("Train object has been parented to: " + raycastParent.name);
                   // Debug.LogError("Detected");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("The raycasting object has no parent to attach the train to.");
                }
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Door") && ShouldDetectDoor == true) {
                //Debug.Log("Ayaz");
                hit.collider.transform.parent.GetComponent<BusBehavior>().PlayDoorAnim();
            }
            
        }
    }
}
