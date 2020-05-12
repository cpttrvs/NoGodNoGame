using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class IsNearWaypoint : NodeCondition
{
    [NodeParam]
    private string waypointKey = null;
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
            Waypoint waypoint = behaviorTree.blackBoard[waypointKey] as Waypoint;
            if(waypoint != null)
            {
                Vector2 characterPos = new Vector2(character.transform.position.x, character.transform.position.z);
                Vector2 objectPos = new Vector2(waypoint.GetPosition().x, waypoint.GetPosition().z);

                float distance = Vector2.Distance(characterPos, objectPos);

                if (distance <= minimalRange)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
