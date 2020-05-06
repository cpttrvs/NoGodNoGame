using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class Garden : Area, IBlackBoardData
{
    [SerializeField]
    private Waypoint _entry = null;
    public Waypoint entry { get { return _entry; } }

    [SerializeField]
    private Waypoint _basketSpot = null;
    public Waypoint basketSpot { get { return _basketSpot; } }

    [SerializeField]
    private List<GardenWaypointsLane> _waypointsLanes = new List<GardenWaypointsLane>();
    public List<GardenWaypointsLane> waypointsLanes { get { return _waypointsLanes; } }
    
    public List<Plant> GetPlants(GardenWaypointsLane specific = null)
    {
        List<Plant> plants = null;
        List<GardenWaypointsLane> lanesToScan = new List<GardenWaypointsLane>();

        if (specific != null)
            lanesToScan.Add(specific);
        else
            lanesToScan.AddRange(waypointsLanes);

        
        foreach (WaypointsLane wl in lanesToScan)
        {
            foreach (Waypoint wp in wl.GetWaypoints())
            {
                if (wp is GardenWaypoint)
                {
                    List<Plant> pl = (wp as GardenWaypoint).connectedPlants;

                    if (plants == null)
                        plants = new List<Plant>();

                    if (pl != null)
                    {
                        foreach (Plant p in pl)
                        {
                            plants.Add(p);
                        }
                    }
                }
            }
        }
        


        return plants;
    }

    public List<Weeds> GetWeeds(GardenWaypointsLane specific = null)
    {
        List<Weeds> weeds = null;

        List<Plant> plants = GetPlants(specific);

        if(plants != null)
        {
            foreach(Plant p in plants)
            {
                if(p is Weeds)
                {
                    if (weeds == null)
                        weeds = new List<Weeds>();

                    weeds.Add((p as Weeds));
                }
            }
        }

        return weeds;
    }

    public int GetRemainingWeeds(GardenWaypointsLane specific = null)
    {
        List<Weeds> weeds = GetWeeds(specific);

        if (weeds != null)
            return weeds.Count;

        return 0;
    }

    public int GetRemainingWeedsToUnplant(GardenWaypointsLane specific = null)
    {
        List<Weeds> weeds = GetWeeds(specific);
        int total = 0;

        if(weeds != null)
        {
            foreach(Weeds w in weeds)
            {
                if (w.isPlanted)
                    total++;
            }
        }

        //Debug.Log("Garden: remaining unplant: " + total);
        return total;
    }

    public int GetRemainingWeedsToPickup(GardenWaypointsLane specific = null)
    {
        List<Weeds> weeds = GetWeeds(specific);
        int total = 0;

        if(weeds != null)
        {
            foreach(Weeds w in weeds)
            {
                if (!w.isPlanted)
                    total++;
            }
        }

        //Debug.Log("Garden: remaining pick up: " + total);
        return total;
    }
}
