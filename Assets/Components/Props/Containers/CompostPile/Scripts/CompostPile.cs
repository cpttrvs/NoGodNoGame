using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostPile : Container
{
    [SerializeField]
    private Waypoint _waypoint = null;
    public Waypoint waypoint { get { return _waypoint; } }
}
