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
    private CompostPile _compostPile = null;
    public CompostPile compostPile { get { return _compostPile; } }

    [SerializeField]
    private Basket _basket = null;
    public Basket basket { get { return _basket; } }

    [SerializeField]
    private ContainerWaypoint _basketOnFootWaypoint = null;
    public ContainerWaypoint basketOnFootWaypoint { get { return _basketOnFootWaypoint; } }
    
    [SerializeField]
    private Container _handsContainer = null;
    public Container handsContainer { get { return _handsContainer; } }

    private bool _isCrawling = false;
    public bool isCrawling { get { return _isCrawling; } }

    [Header("Specific Animations")]
    [SerializeField]
    private string stateCrawlBool = null;
    [SerializeField]
    private string stateWeedTrigger = null;
    [SerializeField]
    private string statePickUpWeedsTrigger = null;
    [SerializeField]
    private string stateDropWeedsTrigger = null;
    [SerializeField]
    private string stateEmptyBasketTrigger = null;


    public GardenWaypointsLane currentGardenWaypointsLane { get; set; }

    public bool Unplant(GardenWaypoint gardenWaypoint)
    {
        bool success = gardenWaypoint.UnplantAny();
        
        if(success)
        {
            //animatorStateMachine.SetTrigger(stateWeedTrigger);
        }

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
            {
                animatorStateMachine.SetTrigger(statePickUpWeedsTrigger);

                return true;
            }
        }

        return false;
    }

    public override bool EmptyContainerInContainer(Container from, Container to)
    {
        if (from == null || to == null) return false;

        bool success = base.EmptyContainerInContainer(from, to);

        if(success)
        {
            if (from == handsContainer)
            {
                animatorStateMachine.SetTrigger(stateDropWeedsTrigger);
            } else if (from == basket)
            {
                animatorStateMachine.SetTrigger(stateEmptyBasketTrigger);
            }
        }

        return success;
    }

    public void Crawl()
    {
        _isCrawling = true;
        animatorStateMachine.SetBool(stateCrawlBool, isCrawling);
    }

    public void StopCrawl()
    {
        _isCrawling = false;
        animatorStateMachine.SetBool(stateCrawlBool, isCrawling);
    }

    public bool Stretch()
    {
        //play animation
        Debug.Log("STRETCH");
        return true;
    }
}
