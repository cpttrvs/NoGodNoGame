using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class FindWaypoint : NodeAction
{
    [NodeParam]
    private bool randomPick = true;
    [NodeParam]
    private bool preventSameNode = true;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        BBList<Waypoint> bbWaypoints = (BBList<Waypoint>) behaviorTree.blackBoard["waypoints"];
        List<Waypoint> waypoints = bbWaypoints.list;

        if(waypoints != null)
        {
            Waypoint pickedWaypoint = null;

            if (randomPick)
            {
                int rand = UnityEngine.Random.Range(0, waypoints.Count);

                pickedWaypoint = waypoints[rand];

                if(preventSameNode)
                {
                    while(pickedWaypoint == (behaviorTree.blackBoard["currentWaypoint"] as Waypoint))
                    {
                        rand = UnityEngine.Random.Range(0, waypoints.Count);

                        pickedWaypoint = waypoints[rand];
                    }
                }
            }
            
            if (pickedWaypoint != null)
            {
                behaviorTree.blackBoard["currentWaypoint"] = pickedWaypoint;

                return BTState.Success;
            }
        }
        
        return BTState.Failure;
    }
}
