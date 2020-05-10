using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class FindClosestBasketWaypoint : NodeAction
{
    [NodeParam]
    private string gardenKey = null;
    [NodeParam]
    private bool inCurrentGardenLane = true;
    [NodeParam]
    private string closestWaypointKey = null;
    [NodeParam]
    private string currentLaneKey = null;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Weeder weeder = behaviorTree.blackBoard["self"] as Weeder;
        Garden garden = behaviorTree.blackBoard[gardenKey] as Garden;

        if (weeder != null && garden != null)
        {
            List<ContainerWaypoint> containerWaypoints = new List<ContainerWaypoint>();

            GardenWaypointsLane currentGardenWaypointsLane = weeder.currentGardenWaypointsLane;

            // select eligible container waypoints
            if (currentGardenWaypointsLane != null)
            {
                if (inCurrentGardenLane)
                {
                    containerWaypoints.AddRange(currentGardenWaypointsLane.basketWaypoints);
                }
                else
                {
                    List<GardenWaypointsLane> gardenWaypointsLanes = garden.waypointsLanes;
                    foreach(GardenWaypointsLane gwpl in gardenWaypointsLanes)
                    {
                        if(gwpl != currentGardenWaypointsLane)
                        {
                            containerWaypoints.AddRange(gwpl.basketWaypoints);
                        }
                    }
                }
            } else
            {
                List<GardenWaypointsLane> gardenWaypointsLanes = garden.waypointsLanes;
                foreach (GardenWaypointsLane gwpl in gardenWaypointsLanes)
                {
                    containerWaypoints.AddRange(gwpl.basketWaypoints);
                }
            }

            if(containerWaypoints != null)
            {
                // pick closest
                ContainerWaypoint closest = containerWaypoints[0];

                foreach(ContainerWaypoint cwp in containerWaypoints)
                {
                    if(Vector3.Distance(weeder.transform.position, cwp.GetPosition()) < Vector3.Distance(weeder.transform.position, closest.GetPosition()))
                    {
                        closest = cwp;
                    }
                }

                if (closest == null)
                {
                    Debug.LogWarning("FindClosestBasketWaypoint: No Basket Waypoint found");

                    behaviorTree.blackBoard[closestWaypointKey] = closest;
                    return BTState.Failure;
                }
                else
                {
                    // memorise the current garden lane
                    foreach (GardenWaypointsLane wpl in garden.waypointsLanes)
                    {
                        foreach(ContainerWaypoint cwp in wpl.basketWaypoints)
                        {
                            if(cwp == closest)
                            {
                                weeder.currentGardenWaypointsLane = wpl;
                                behaviorTree.blackBoard[currentLaneKey] = weeder.currentGardenWaypointsLane;
                                break;
                            }
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
