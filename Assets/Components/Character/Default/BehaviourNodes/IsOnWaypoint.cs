using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class IsOnWaypoint : NodeCondition
{
    [NodeParam]
    private float minimalRange = 1f;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if(character != null)
        {
            Waypoint waypoint = behaviorTree.blackBoard["currentWaypoint"] as Waypoint;
            if(waypoint != null)
            {
                float distance = Vector3.Distance(character.transform.position, waypoint.GetPosition());

                if(distance <= minimalRange)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
