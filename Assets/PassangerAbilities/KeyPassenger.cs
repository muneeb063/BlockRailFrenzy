using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.BusStop;
[CreateAssetMenu(fileName = "CharacterAction", menuName = "Passenger Actions/Key Character")]
public class KeyPassenger : ScriptableObject, IPassengerAction
{
    public void Apply(HumanoidCharacterBehavior passenger)
    {
        passenger.isKeyPassenger = true;
        passenger.KeyModel.SetActive(true);
     
    }
}

