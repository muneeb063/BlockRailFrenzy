using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.BusStop;
[CreateAssetMenu(fileName = "CharacterAction", menuName = "Passenger Actions/Bomb Character")]
public class BombPassenger : ScriptableObject, IPassengerAction
{
    public void Apply(HumanoidCharacterBehavior passenger)
    {
        if (passenger == null) return;
        passenger.BombHead.SetActive(true);
        passenger.IsBombed = true;
        PassengerActionManager.instance.BombPassengers.Add(passenger);

    }
}
