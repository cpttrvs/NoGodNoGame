using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasWaypoints
{
    List<Waypoint> waypoints { get; }
    Waypoint currentWaypoint { get; set; }
    Waypoint lastWaypoint { get; set; }
}
