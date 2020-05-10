using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class UnplantWeeds : NodeAction
{
    [NodeParam]
    private bool currentWaypoint = true;

    private bool actionCompleted = false;
    private bool actionStarted = false;

    public override void Cleanup()
    {
        actionCompleted = false;
        actionStarted = false;
        
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Weeder weeder = (Weeder) behaviorTree.blackBoard["self"];

        if(weeder != null)
        {
            if(!actionStarted)
            {
                if (currentWaypoint)
                {
                    if (weeder.currentWaypoint is GardenWaypoint)
                    {
                        GardenWaypoint gw = weeder.currentWaypoint as GardenWaypoint;

                        bool success = weeder.Unplant(gw);

                        if (success)
                        {
                            actionStarted = true;

                            weeder.OnActionCompleted += Character_OnActionCompleted;

                            return BTState.Running;
                        }
                    }
                }
            }
            else
            {
                if (!actionCompleted)
                {
                    return BTState.Running;
                }
                else
                {
                    return BTState.Success;
                }
            }
        }
        

        return BTState.Failure;
    }

    private void Character_OnActionCompleted(Character arg1, bool arg2)
    {
        if (arg2) actionCompleted = true;

        arg1.OnActionCompleted -= Character_OnActionCompleted;
    }
}
