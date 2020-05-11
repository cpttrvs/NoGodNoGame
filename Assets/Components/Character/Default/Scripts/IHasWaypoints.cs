using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasWaypoints
{
    Waypoint currentWaypoint { get; set; }
    Waypoint lastWaypoint { get; set; }
}
