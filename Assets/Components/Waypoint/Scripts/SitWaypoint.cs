using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitWaypoint : Waypoint
{
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, new Vector3(
            transform.position.x + transform.forward.x, 
            transform.position.y + transform.forward.y,
            transform.position.z + transform.forward.z));
    }
}
