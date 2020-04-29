using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsLane : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> _waypoints = new List<Waypoint>();

    [SerializeField]
    private Waypoint _startWaypoint = null;
    [SerializeField]
    private Waypoint _endWaypoint = null;

    public List<Waypoint> GetWaypoints() { return _waypoints; }
    public Waypoint GetStartWaypoint() { return _startWaypoint; }
    public Waypoint GetEndWaypoint() { return _endWaypoint; }
}
