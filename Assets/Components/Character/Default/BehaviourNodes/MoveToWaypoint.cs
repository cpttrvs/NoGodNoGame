using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class MoveToWaypoint : NodeAction
{
    [NodeParam]
    private string waypointKey = null;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        IBlackBoardData obj = behaviorTree.blackBoard["self"];

        if(obj is IMovable)
        {
            IMovable movable = obj as IMovable;

            Waypoint waypoint = behaviorTree.blackBoard[waypointKey] as Waypoint;

            if (waypoint != null)
            {
                if (obj is Character)
                {
                    (obj as Character).lastWaypoint = (obj as Character).currentWaypoint;
                    (obj as Character).currentWaypoint = waypoint;
                }

                movable.MoveTo(waypoint.GetPosition());

                return BTState.Success;
            }
        }

        return BTState.Failure;
    }
}
