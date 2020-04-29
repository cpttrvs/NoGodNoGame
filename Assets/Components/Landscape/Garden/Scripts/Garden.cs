using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class Garden : MonoBehaviour, IBlackBoardData
{
    [SerializeField]
    private Waypoint _entry = null;
    public Waypoint entry { get { return _entry; } }

    [SerializeField]
    private Waypoint _basketSpot = null;
    public Waypoint basketSpot { get { return _basketSpot; } }

    [SerializeField]
    private List<WaypointsLane> _waypointsLanes = new List<WaypointsLane>();
    public List<WaypointsLane> waypointsLanes { get { return _waypointsLanes; } }

    public List<Plant> GetPlants()
    {
        List<Plant> plants = null;
        foreach(WaypointsLane wl in waypointsLanes)
        {
            foreach(Waypoint wp in wl.GetWaypoints())
            {
                if(wp is GardenWaypoint)
                {
                    List<Plant> pl = (wp as GardenWaypoint).connectedPlants;

                    if (plants == null)
                        plants = new List<Plant>();

                    if(pl != null)
                    {
                        foreach(Plant p in pl)
                        {
                            plants.Add(p);
                        }
                    }
                }
            }
        }

        return plants;
    }

    public List<Weeds> GetWeeds()
    {
        List<Weeds> weeds = null;

        List<Plant> plants = GetPlants();

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

    public int GetRemainingWeeds()
    {
        List<Weeds> weeds = GetWeeds();

        if (weeds != null)
            return weeds.Count;

        return 0;
    }
}
