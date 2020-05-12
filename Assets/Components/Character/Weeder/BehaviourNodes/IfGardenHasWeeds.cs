using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IfGardenHasWeeds : NodeCondition
{
    [Header("Pick one at most")]
    [NodeParam]
    bool weedsToUnplant = false;
    [NodeParam]
    bool weedsToPickup = false;

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
                
                if (weedsToUnplant)
                    return garden.GetRemainingWeedsToUnplant() > 0;

                if (weedsToPickup)
                    return garden.GetRemainingWeedsToPickup() > 0;

                return garden.GetRemainingWeeds() > 0;
            }
        }

        return false;
    }
}
