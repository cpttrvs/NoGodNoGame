﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerWaypoint : Waypoint
{
    protected Container _container;
    public Container container { get { return _container; } }

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
    }
}
