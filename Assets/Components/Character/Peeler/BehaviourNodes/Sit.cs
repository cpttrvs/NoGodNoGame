using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class Sit : NodeAction
{
    [NodeParam]
    private string sitWaypointKey = null;
    [NodeParam]
    private bool stand = false;

    private bool actionCompleted = false;
    private bool actionStarted = false;

    public override void Cleanup()
    {
        actionCompleted = false;
        actionStarted = false;

    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Peeler peeler = behaviorTree.blackBoard["self"] as Peeler;

        if (peeler != null)
        {
            if(!actionStarted)
            {
                actionStarted = true;

                peeler.OnActionCompleted += Character_OnActionCompleted;

                if (stand)
                    peeler.Stand();
                else
                {
                    SitWaypoint sitWaypoint = behaviorTree.blackBoard[sitWaypointKey] as SitWaypoint;

                    peeler.Sit(sitWaypoint);
                }

                return BTState.Running;
            }
            else
            {
                if(!actionCompleted)
                {
                    return BTState.Running;
                } else
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
