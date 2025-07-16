using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.BusStop;
[CreateAssetMenu(fileName = "CharacterAction", menuName = "Passenger Actions/Lock Character")]
public class LockPassanger : ScriptableObject, IPassengerAction
{
    public void Apply(HumanoidCharacterBehavior passenger)
    {
        passenger.isLocked = true;
        passenger.LockModel.SetActive(true);
        passenger.gameObject.GetComponent<BoxCollider>().enabled = false;
        PassengerActionManager.instance.LockedCharacters.Add(passenger);

    }
}
