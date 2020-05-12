using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class StopPeeling : NodeAction
{
    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Peeler peeler = behaviorTree.blackBoard["self"] as Peeler;

        if (peeler != null)
        {
            peeler.StopPeeling();

            return BTState.Success;
        }

        return BTState.Failure;
    }
}
