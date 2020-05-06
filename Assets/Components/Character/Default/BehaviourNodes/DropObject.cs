using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class DropObject : NodeAction
{
    [NodeParam]
    private string objectKey = null;
    [NodeParam]
    private string dropWaypointKey = null;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        IBlackBoardData obj = behaviorTree.blackBoard["self"];

        if (obj is ICanCarry)
        {
            ICanCarry canCarry = obj as ICanCarry;

            ICarriable carriable = behaviorTree.blackBoard[objectKey] as ICarriable;

            ContainerWaypoint dropWaypoint = behaviorTree.blackBoard[dropWaypointKey] as ContainerWaypoint;

            if (carriable != null && dropWaypoint != null)
            {
                bool res = canCarry.Drop(carriable, dropWaypoint.transform);

                if (res) return BTState.Success;
                else return BTState.Failure;
            }
        }

        return BTState.Failure;
    }
}
