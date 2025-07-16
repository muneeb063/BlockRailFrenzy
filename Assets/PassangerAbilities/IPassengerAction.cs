using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.BusStop;

public interface IPassengerAction
{
    void Apply(HumanoidCharacterBehavior passenger);
}

