using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peeler : Character
{
    [Header("Peeler")]
    [SerializeField]
    private Basket _basketVegetables = null;
    public Basket basketVegetables { get { return _basketVegetables; } }
    [SerializeField]
    private ContainerWaypoint _basketVegetablesWaypoint = null;
    public ContainerWaypoint basketVegetablesWaypoint { get { return _basketVegetablesWaypoint; } }

    [SerializeField]
    private Basket _basketPeeled = null;
    public Basket basketPeeled { get { return _basketPeeled; } }
    [SerializeField]
    private ContainerWaypoint _basketPeeledWaypoint = null;
    public ContainerWaypoint basketPeeledWaypoint { get { return _basketPeeledWaypoint; } }

    [SerializeField]
    private SitWaypoint _benchWaypoint = null;
    public SitWaypoint benchWaypoint { get { return _benchWaypoint; } }

    [SerializeField]
    private ContainerWaypoint _basketOnFootWaypoint = null;
    public ContainerWaypoint basketOnFootWaypoint { get { return _basketOnFootWaypoint; } }

    [SerializeField]
    private Container _handsContainer = null;
    public Container handsContainer { get { return _handsContainer; } }

    [Header("Specific Animations")]
    [SerializeField]
    private string stateSitBool = null;
    [SerializeField]
    private string statePeelTrigger = null;


    private bool _isSitted = false;
    public bool isSitted { get { return _isSitted; } }

    // Peel
    public bool PeelAny(Basket basket)
    {
        if(basketVegetables.GetContentSize() > 0)
        {
            this.OnAnimationCompleted += PeelAnyDelegate;
            animatorStateMachine.SetTrigger(statePeelTrigger);

            return true;
        }

        return false;
    }
    private void PeelAnyDelegate(Character c, string s)
    {
        Debug.Log("PEEL DELEGATE");
        bool success = true;
        OnActionComplete(success);

        this.OnAnimationCompleted -= PeelAnyDelegate;
    }

    // Sit
    public bool Sit(SitWaypoint sitWaypoint)
    {
        if (isSitted) return false;

        //check if on waypoint?

        //smooth rotation to do
        transform.rotation = Quaternion.LookRotation(sitWaypoint.faceDirection);

        _isSitted = true;
        this.OnAnimationCompleted += SitDelegate;
        animatorStateMachine.SetBool(stateSitBool, true);

        return true;
    }
    private void SitDelegate(Character c, string s)
    {
        Debug.Log("SIT DELEGATE");
        OnActionComplete(true);

        this.OnAnimationCompleted -= SitDelegate;
    }

    // Stand
    public bool Stand()
    {
        if (!isSitted) return false;

        _isSitted = false;
        this.OnAnimationCompleted += StandDelegate;
        animatorStateMachine.SetBool(stateSitBool, false);

        return true;
    }
    private void StandDelegate(Character c, string s)
    {
        Debug.Log("STAND DELEGATE");
        OnActionComplete(true);

        this.OnAnimationCompleted -= StandDelegate;
    }
}
