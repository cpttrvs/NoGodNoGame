using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IfGardenLaneHasWeeds : NodeCondition
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
            if (character is Weeder)
            {
                Weeder weeder = character as Weeder;

                Garden garden = weeder.garden;

                if(weeder.currentGardenWaypointsLane == null)
                {
                    Debug.LogWarning("IfGardenLaneHasWeeds: no memorised lane");
                    return false;
                }
                
                if (weedsToUnplant)
                    return garden.GetRemainingWeedsToUnplant(weeder.currentGardenWaypointsLane) > 0;

                if (weedsToPickup)
                    return garden.GetRemainingWeedsToPickup(weeder.currentGardenWaypointsLane) > 0;

                return garden.GetRemainingWeeds(weeder.currentGardenWaypointsLane) > 0;
            }
        }

        return false;
    }
}
