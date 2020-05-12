using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerWaypoint : Waypoint
{
    protected Container _container;
    public Container container { get { return _container; } }

    private void Start()
    {
        
    }

    public bool IsEmpty()
    {
        return container == null;
    }

    public bool PlaceContainer(Container c)
    {
        if(IsEmpty())
        {
            _container = c;
            return true;
        }

        if (_container == c)
        {
            Debug.LogWarning("ContainerWaypoint: " + name + " same container placed: " + c.name);
            return false;
        }

        return false;
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, new Vector3(
            transform.position.x + transform.forward.x,
            transform.position.y + transform.forward.y,
            transform.position.z + transform.forward.z));
    }
}
