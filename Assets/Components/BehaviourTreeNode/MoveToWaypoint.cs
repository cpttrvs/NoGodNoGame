using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class MoveToWaypoint : NodeAction
{
    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        IBlackBoardData obj = behaviorTree.blackBoard["self"];

        if(obj is IMovable)
        {
            IMovable movable = obj as IMovable;

            Waypoint waypoint = behaviorTree.blackBoard["currentWaypoint"] as Waypoint;

            if (waypoint != null)
            {
                movable.MoveTo(waypoint.GetPosition());

                return BTState.Success;
            }
        }

        return BTState.Failure;
    }
}
