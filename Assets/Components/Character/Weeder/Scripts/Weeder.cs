using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private string stateInterruptWeedTrigger = null;
    [SerializeField]
    private string statePickUpWeedsTrigger = null;
    [SerializeField]
    private string stateDropWeedsTrigger = null;
    [SerializeField]
    private string stateEmptyBasketTrigger = null;


    public GardenWaypointsLane currentGardenWaypointsLane { get; set; }
    
    // Unplant
    private GardenWaypoint unplantGardenWaypoint;
    public bool Unplant(GardenWaypoint gardenWaypoint)
    {
        if(gardenWaypoint.HasWorkUnplant())
        {
            unplantGardenWaypoint = gardenWaypoint;

            this.OnAnimationCompleted += UnplantDelegate;
            animatorStateMachine.SetTrigger(stateWeedTrigger);
            
            return true;
        }

        return false;
    }
    private void UnplantDelegate(Character c, string s)
    {
        //Debug.Log("UNPLANT DELEGATE");
        bool success = unplantGardenWaypoint.UnplantAny();
        OnActionComplete(success);

        this.OnAnimationCompleted -= UnplantDelegate;
    }

    // Pickup
    private GardenWaypoint pickupGardenWaypoint;
    public bool Pickup(GardenWaypoint gardenWaypoint)
    {
        if (handsContainer.IsFull())
            return false;
        
        if (gardenWaypoint.HasWorkPickUp())
        {
            pickupGardenWaypoint = gardenWaypoint;

            this.OnAnimationCompleted += PickupDelegate;
            animatorStateMachine.SetTrigger(statePickUpWeedsTrigger);

            return true;
        }

        return false;
    }
    private void PickupDelegate(Character c, string s)
    {
        Debug.Log("PICK UP DELEGATE");
        Plant p = pickupGardenWaypoint.PickupAny();

        bool success = false;

        if (p != null)
        {
            success = handsContainer.AddItem(p);
        }
        
        OnActionComplete(success);
        this.OnAnimationCompleted -= PickupDelegate;
    }

    // Empty Container in Container
    public override bool EmptyContainerInContainer(Container from, Container to)
    {
        if (from == null || to == null) return false;

        bool success = base.EmptyContainerInContainer(from, to);

        if(success)
        {
            if (from == handsContainer)
            {
                this.OnAnimationCompleted += EmptyContainerInContainerDelegate;
                animatorStateMachine.SetTrigger(stateDropWeedsTrigger);
            } else if (from == basket)
            {
                this.OnAnimationCompleted += EmptyContainerInContainerDelegate;
                animatorStateMachine.SetTrigger(stateEmptyBasketTrigger);
            }
        }

        return success;
    }
    private void EmptyContainerInContainerDelegate(Character c, string s)
    {
        //Debug.Log("EMPTY CONTAINER DELEGATE");
        OnActionComplete(true);

        this.OnAnimationCompleted -= EmptyContainerInContainerDelegate;
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

    public void StopWeeding()
    {
        animatorStateMachine.SetTrigger(stateInterruptWeedTrigger);
    }

    protected override void UnregisterDelegates()
    {
        base.UnregisterDelegates();

        this.OnAnimationCompleted -= UnplantDelegate;
        this.OnAnimationCompleted -= PickupDelegate;

        this.OnAnimationCompleted -= EmptyContainerInContainerDelegate;
    }
}
