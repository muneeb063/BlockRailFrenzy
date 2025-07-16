// File: FreezeCharacterActionSO.cs

using UnityEngine;
using Watermelon.BusStop;

[CreateAssetMenu(fileName = "FreezeCharacterAction", menuName = "Passenger Actions/Freeze Character")]
public class FreezeCharacterActionSO : ScriptableObject,IPassengerAction
{
    public Material frozenMaterial;

    public void Apply(HumanoidCharacterBehavior passenger)
    {
        
        // Apply new material
        if (passenger == null) return;
        Renderer renderer = passenger.characterRenderer;
        if (renderer == null)
            renderer = passenger.GetComponentInChildren<Renderer>();
        passenger.originalMaterial = renderer.material;
        passenger.freezeUI.SetActive(true);
        //passenger.freezeCounter = 3;
        
        passenger.isFrozen = true;
        PassengerActionManager.instance.FreezedPassengers.Add(passenger);
        if (renderer != null && frozenMaterial != null)
        {
            renderer.material = frozenMaterial;
            
        }
        passenger.gameObject.GetComponent<BoxCollider>().enabled = false;
        // Disable the behavior script
        HumanoidCharacterBehavior behavior = passenger.GetComponent<HumanoidCharacterBehavior>();
        if (behavior != null)
        {
            behavior.enabled = false;
        }
    }
}
