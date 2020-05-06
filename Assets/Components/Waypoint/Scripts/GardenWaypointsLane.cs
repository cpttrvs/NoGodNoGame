using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenWaypointsLane : WaypointsLane
{
    [SerializeField]
    private List<ContainerWaypoint> _basketWaypoints = new List<ContainerWaypoint>();
    public List<ContainerWaypoint> basketWaypoints { get { return _basketWaypoints; } }

    public bool PlaceContainer(ContainerWaypoint cw, Container c)
    {
        if(cw.IsEmpty() && _basketWaypoints.Contains(cw))
        {
            return cw.PlaceContainer(c);
        }

        return false;
    }
}
