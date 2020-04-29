using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenWaypoint : Waypoint
{
    [SerializeField]
    private List<Plant> _connectedPlants = new List<Plant>();
    public List<Plant> connectedPlants { get { return _connectedPlants; } }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
