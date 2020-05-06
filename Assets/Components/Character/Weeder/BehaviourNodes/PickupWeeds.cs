using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class PickupWeeds : NodeAction
{
    [NodeParam]
    private bool currentWaypoint = true;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Weeder weeder = (Weeder)behaviorTree.blackBoard["self"];

        if (weeder != null)
        {
            if (currentWaypoint)
            {
                if (weeder.currentWaypoint is GardenWaypoint)
                {
                    GardenWaypoint gw = weeder.currentWaypoint as GardenWaypoint;

                    bool success = weeder.Pickup(gw);

                    if (success)
                    {
                        return BTState.Success;
                    }
                }
            }
        }


        return BTState.Failure;
    }
}
