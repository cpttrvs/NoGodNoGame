using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class FindClosestGardenWaypoint : NodeAction
{
    [NodeParam]
    private string gardenKey = null;
    [NodeParam]
    private string closestWaypointKey = null;
    [NodeParam]
    private string currentLaneKey = null;
    [Header("Work (if unplant is false, it is about collecting)")]
    [NodeParam]
    private bool unplant = false;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Weeder weeder = behaviorTree.blackBoard["self"] as Weeder;

        if (weeder != null)
        {
            Garden garden = behaviorTree.blackBoard[gardenKey] as Garden;

            List<GardenWaypointsLane> waypointsLanes = garden.waypointsLanes;
            
            if (waypointsLanes != null)
            {
                List<Waypoint> waypoints = new List<Waypoint>();

                foreach (WaypointsLane wpl in waypointsLanes)
                {
                    foreach(Waypoint wp in wpl.GetWaypoints())
                    {
                        waypoints.Add(wp);
                    }
                }

                Waypoint closest = null;
                Vector3 characterPosition = weeder.transform.position;
                
                foreach(Waypoint wp in waypoints)
                {
                    if (closest == null)
                    {
                        if (wp is GardenWaypoint)
                        {
                            if(unplant && (wp as GardenWaypoint).HasWorkUnplant())
                            {
                                closest = wp;
                            }

                            if(!unplant && (wp as GardenWaypoint).HasWorkPickUp())
                            {
                                closest = wp;
                            }
                        }
                    } else
                    {
                        if (Vector3.Distance(characterPosition, wp.GetPosition()) <= Vector3.Distance(characterPosition, closest.GetPosition()))
                        {
                            if (wp is GardenWaypoint)
                            {
                                if (unplant && (wp as GardenWaypoint).HasWorkUnplant())
                                {
                                    closest = wp;
                                }

                                if (!unplant && (wp as GardenWaypoint).HasWorkPickUp())
                                {
                                    closest = wp;
                                }
                            }
                            else
                            {
                                Debug.LogWarning("FindClosestGardenWaypoint NOT GardenWaypoint: " + wp.name);
                            }
                        }
                    }
                }
                
                if(closest == null)
                {
                    Debug.Log("FindClosestGardenWaypoint: Nothing more");
                    return BTState.Success;
                } else
                {
                    // memorise the current garden lane
                    foreach (GardenWaypointsLane wpl in waypointsLanes)
                    {
                        if (wpl.GetWaypoints().Contains(closest))
                        {
                            weeder.currentGardenWaypointsLane = wpl;
                            behaviorTree.blackBoard[currentLaneKey] = weeder.currentGardenWaypointsLane;
                            break;
                        }
                    }

                    behaviorTree.blackBoard[closestWaypointKey] = closest;
                    return BTState.Success;
                }
            }
        }

        return BTState.Failure;
    }
}
