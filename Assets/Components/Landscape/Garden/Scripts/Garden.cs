using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField]
    private Waypoint _entry = null;
    public Waypoint entry { get { return _entry; } }

    [SerializeField]
    private List<WaypointsLane> _waypointsLanes = new List<WaypointsLane>();
    public List<WaypointsLane> waypointsLanes { get { return _waypointsLanes; } }
}
