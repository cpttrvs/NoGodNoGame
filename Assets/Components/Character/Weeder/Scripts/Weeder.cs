using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weeder : Character
{
    [Header("Weeder")]
    [SerializeField]
    private Garden _garden = null;
    public Garden garden { get { return _garden; } }

    [SerializeField]
    private Basket _basket = null;
    public Basket basket { get { return _basket; } }
    
    [SerializeField]
    private Container _handsContainer = null;
    public Container handsContainer { get { return _handsContainer; } }
    
    public GardenWaypointsLane currentGardenWaypointsLane { get; set; }

    public bool Unplant(GardenWaypoint gardenWaypoint)
    {
        //animation
        bool success = gardenWaypoint.UnplantAny();
        //Debug.Log("Weeder: Unplant " + gardenWaypoint.name + " => " + success);


        return success;
    }

    public bool Pickup(GardenWaypoint gardenWaypoint)
    {
        if (handsContainer.IsFull())
            return false;
        
        Plant p = gardenWaypoint.PickupAny();

        if(p != null)
        {
            bool success = handsContainer.AddItem(p);

            if (success)
                return true;
        }

        return false;
    }
}
