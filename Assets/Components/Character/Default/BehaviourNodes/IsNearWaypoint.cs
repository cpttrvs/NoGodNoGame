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
    [NodeParam]
    private bool autoStop = false;
    [NodeParam]
    private bool faceDirection = false;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if(character != null)
        {
            Waypoint waypoint = behaviorTree.blackBoard[waypointKey] as Waypoint;
            Vector3 waypointPos = waypoint.GetPosition();
            if(waypoint != null)
            {
                Vector2 characterPos = new Vector2(character.transform.position.x, character.transform.position.z);
                Vector2 objectPos = new Vector2(waypointPos.x, waypointPos.z);

                float distance = Vector2.Distance(characterPos, objectPos);

                if (distance <= minimalRange)
                {
                    if (autoStop)
                        character.Stop();

                    if(faceDirection)
                    {
                        /*
                        Debug.Log("NearWaypoint FACE");
                        Quaternion q = new Quaternion(waypoint.faceDirection.x, character.transform.position.y, waypoint.faceDirection.z, 1f);

                        character.transform.rotation = Quaternion.Lerp(character.transform.rotation, q, 0.05f * Time.deltaTime);
                        */

                        character.transform.LookAt(new Vector3(waypointPos.x, character.transform.position.y, waypointPos.z));
                    }

                    return true;
                }
            }
        }

        return false;
    }
}
