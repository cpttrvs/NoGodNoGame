using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class StopWeeding : NodeAction
{
    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Weeder weeder = behaviorTree.blackBoard["self"] as Weeder;

        if(weeder != null)
        {
            weeder.StopWeeding();

            return BTState.Success;
        }

        return BTState.Failure;
    }
}
