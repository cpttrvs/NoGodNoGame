using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IfGardenHasWeeds : NodeCondition
{

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if (character != null)
        {
            if(character is Weeder)
            {
                Weeder weeder = character as Weeder;

                Garden garden = weeder.garden;
                Debug.Log(garden.GetRemainingWeeds());
                return garden.GetRemainingWeeds() > 0;
            }
        }

        return false;
    }
}
