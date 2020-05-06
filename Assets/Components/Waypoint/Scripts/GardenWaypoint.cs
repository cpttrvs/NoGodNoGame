using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenWaypoint : Waypoint
{
    [SerializeField]
    private List<Plant> _connectedPlants = new List<Plant>();
    public List<Plant> connectedPlants { get { return _connectedPlants; } }

    public bool UnplantAny()
    {
        foreach(Plant p in connectedPlants)
        {
            if(p.isPlanted)
            {
                p.Unplant();
                return true;
            }
        }
        return false;
    }

    public Plant PickupAny()
    {
        foreach(Plant p in connectedPlants)
        {
            if(!p.isPlanted)
            {
                _connectedPlants.Remove(p);
                return p;
            }
        }

        return null;
    }

    public bool HasWork()
    {
        return connectedPlants.Count > 0;
    }

    public bool HasWorkUnplant()
    {
        foreach(Plant p in connectedPlants)
        {
            if(p.isPlanted)
            {
                return true;
            }
        }

        return false;
    }

    public bool HasWorkCollect()
    {
        foreach (Plant p in connectedPlants)
        {
            if (!p.isPlanted)
            {
                return true;
            }
        }

        return false;
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
