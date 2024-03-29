﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AillieoUtils.EasyBehaviorTree;
using System;

public class Character : MonoBehaviour, IBlackBoardData, IMovable, IHasWaypoints, IClickable, ICanCarry
{
    [Header("Animations")]
    [SerializeField]
    protected Animator animatorStateMachine = null;
    [SerializeField]
    private string stateWalkBool = null;
    [SerializeField]
    private string stateOnClickTrigger = null;
    [SerializeField]
    private string stateDropItemTrigger = null;
    [SerializeField]
    private string statePickUpItemTrigger = null;

    public event Action<Character, string> OnAnimationCompleted;
    public event Action<Character, string> OnAnimationStretchCompleted;
    public event Action<Character, string> OnAnimationEventFired;

    [Header("State Machine")]
    [SerializeField]
    private Animator stateMachine = null;
    [SerializeField]
    private string onClickTrigger = null;
    [SerializeField]
    private string onRainTrigger = null;

    public Animator GetStateMachine() { return stateMachine; }
    public event Action<Character, bool> OnActionCompleted;

    [SerializeField]
    private float cooldownClick = 10f;
    private bool onCooldown = false;

    [Header("Nav Mesh")]
    [SerializeField]
    protected NavMeshAgent navMeshAgent = null;
    
    [Header("Can Carry")]
    [SerializeField]
    private uint _carryingCapacity = 0;
    public uint carryingCapacity { get { return _carryingCapacity; } }

    [SerializeField]
    private List<Transform> _carryingSlots = new List<Transform>();
    public List<Transform> carryingSlots { get { return _carryingSlots; } }

    [Header("Home")]
    [SerializeField]
    private Waypoint _homeDoorWaypoint = null;
    public Waypoint homeDoorWaypoint { get { return _homeDoorWaypoint; } }
    [SerializeField]
    private Waypoint _homeInsideWaypoint = null;
    public Waypoint homeInsideWaypoint { get { return _homeInsideWaypoint; } }
    [SerializeField]
    private ContainerWaypoint _homeContainerWaypoint = null;
    public ContainerWaypoint homeContainerWaypoint { get { return _homeContainerWaypoint; } }
    
    // IHasWaypoints
    public Waypoint currentWaypoint { get; set; }
    public Waypoint lastWaypoint { get; set; }

    // IMovable
    public void MoveTo(Vector3 to)
    {
        navMeshAgent.enabled = true;

        navMeshAgent.isStopped = false;

        navMeshAgent.SetDestination(to);

        animatorStateMachine.SetBool(stateWalkBool, true);
    }

    public void Stop()
    {
        if(carryFired)
        {
            carryFired = false;

            (savedCarriableCarry as MonoBehaviour).transform.SetParent(carryingSlots[0]);
            (savedCarriableCarry as MonoBehaviour).transform.localPosition = Vector3.zero;
            (savedCarriableCarry as MonoBehaviour).transform.localRotation = Quaternion.identity;
        }

        if(dropFired)
        {
            dropFired = false;

            (savedCarriableDrop as MonoBehaviour).transform.SetParent(savedPos);
            (savedCarriableDrop as MonoBehaviour).transform.localPosition = Vector3.zero;
            (savedCarriableDrop as MonoBehaviour).transform.localRotation = Quaternion.identity;
        }

        animatorStateMachine.SetBool(stateWalkBool, false);

        //navMeshAgent.isStopped = true;

        //navMeshAgent.ResetPath();
        
        navMeshAgent.enabled = false;
    }
    
    // IClickable
    public void OnMouseDown()
    {
        if(!onCooldown)
        {
            Debug.Log("Character " + name + ": On Click");
            StartCoroutine(StartCooldown());
            stateMachine.SetTrigger(onClickTrigger);
        }
    }

    private IEnumerator StartCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownClick);
        onCooldown = false;
    }

    // ICanCarry
    protected List<ICarriable> _carrying = new List<ICarriable>();
    public List<ICarriable> carrying { get { return _carrying; } }

    private ICarriable savedCarriableCarry = null;
    private bool carryFired = false;
    public bool Carry(ICarriable carriable)
    {
        if (carriable.isCarried) return false;
        if (carrying.Count >= carryingCapacity) return false;

        bool canCarry = carriable.Carry(this);

        if(canCarry)
        {
            //Debug.Log(this.name + " Carry Start");

            this.OnAnimationCompleted += CarryDelegate;
            this.OnAnimationEventFired += CarryEvent;

            carryFired = true;

            savedCarriableCarry = carriable;
            _carrying.Add(savedCarriableCarry);

            animatorStateMachine.SetTrigger(statePickUpItemTrigger);
            
            return true;
        }

        return false;
    }
    private void CarryEvent(Character c, string s)
    {
        //Debug.Log(this.name + " Carry Event");

        this.OnAnimationEventFired -= CarryEvent;
        
        if (savedCarriableCarry is MonoBehaviour)
        {
            carryFired = false;

            (savedCarriableCarry as MonoBehaviour).transform.SetParent(carryingSlots[0]);
            (savedCarriableCarry as MonoBehaviour).transform.localPosition = Vector3.zero;
            (savedCarriableCarry as MonoBehaviour).transform.localRotation = Quaternion.identity;
        }
    }
    private void CarryDelegate(Character c, string s)
    {
        //Debug.Log(this.name + " Carry Delegate");

        if (savedCarriableCarry is MonoBehaviour)
        {
            carryFired = false;

            (savedCarriableCarry as MonoBehaviour).transform.SetParent(carryingSlots[0]);
            (savedCarriableCarry as MonoBehaviour).transform.localPosition = Vector3.zero;
            (savedCarriableCarry as MonoBehaviour).transform.localRotation = Quaternion.identity;
        }

        OnActionComplete(true);
        this.OnAnimationCompleted -= CarryDelegate;
    }

    private Transform savedPos = null;
    private ICarriable savedCarriableDrop = null;
    private bool dropFired = false;
    public bool Drop(ICarriable carriable, Transform pos)
    {
        if (!carriable.isCarried) return false;
        if (carriable.isCarriedBy != this) return false;
        if (!carrying.Contains(carriable)) return false;

        bool canDrop = carriable.Drop();

        if(canDrop)
        {
            //Debug.Log(this.name + " Drop Start");

            this.OnAnimationCompleted += DropDelegate;
            this.OnAnimationEventFired += DropEvent;

            dropFired = true;

            savedCarriableDrop = carriable;
            savedPos = pos;

            _carrying.Remove(savedCarriableDrop);
            
            animatorStateMachine.SetTrigger(stateDropItemTrigger);
            
            return true;
        }

        return false;
    }
    private void DropEvent(Character c, string s)
    {
        //Debug.Log(this.name + " Drop Event");

        this.OnAnimationEventFired -= DropEvent;

        if (savedCarriableDrop is MonoBehaviour)
        {
            dropFired = false;

            (savedCarriableDrop as MonoBehaviour).transform.SetParent(savedPos);
            (savedCarriableDrop as MonoBehaviour).transform.localPosition = Vector3.zero;
            (savedCarriableDrop as MonoBehaviour).transform.localRotation = Quaternion.identity;
        }
    }
    private void DropDelegate(Character c, string s)
    {
        //Debug.Log(this.name + " Drop Delegate");

        if (savedCarriableDrop is MonoBehaviour)
        {
            dropFired = false;

            (savedCarriableDrop as MonoBehaviour).transform.SetParent(savedPos);
            (savedCarriableDrop as MonoBehaviour).transform.localPosition = Vector3.zero;
            (savedCarriableDrop as MonoBehaviour).transform.localRotation = Quaternion.identity;
        }

        OnActionComplete(true);

        this.OnAnimationCompleted -= DropDelegate;
    }

    // Character
    
    public virtual bool EmptyContainerInContainer(Container from, Container to)
    {
        if (from == null || to == null) return false;

        foreach(IContainable containable in from.GetItems())
        {
            if(to.IsFull())
            {
                Debug.Log("Character " + name + ": emptying container " + from.name + " to " + to.name + " (full)");
                break;
            }

            if(from.RemoveItem(containable))
            {
                to.AddItem(containable);
            }
        }

        return true;
    }
    
    public bool Stretch()
    {
        UnregisterDelegates();

        this.OnAnimationStretchCompleted += StretchDelegate;

        animatorStateMachine.SetTrigger(stateOnClickTrigger);

        return true;
    }
    private void StretchDelegate(Character c, string s)
    {
        //Debug.Log("STRETCH DELEGATE");
    
        OnActionComplete(true);

        this.OnAnimationStretchCompleted -= StretchDelegate;
    }

    public void NoticeRain()
    {
        Debug.Log("Character " + name + ": notices rain");
        UnregisterDelegates();

        stateMachine.SetTrigger(onRainTrigger);
    }

    public void OnAnimationComplete(string key)
    {
        //Debug.Log("Character: On Animation Complete " + key);
        OnAnimationCompleted?.Invoke(this, key);
    }

    public void OnAnimationEvent(string key)
    {
        OnAnimationEventFired?.Invoke(this, key);
    }

    public void OnAnimationStretchComplete(string key)
    {
        OnAnimationStretchCompleted(this, key);
    }

    protected void OnActionComplete(bool success)
    {
        //Debug.Log("Character: On Action Complete " + success);
        OnActionCompleted?.Invoke(this, success);
    }

    protected virtual void UnregisterDelegates()
    {
        //Debug.Log("Character " + name + ": unregister delegates");
        this.OnAnimationCompleted -= CarryDelegate;
        this.OnAnimationEventFired -= CarryEvent;
        
        this.OnAnimationCompleted -= DropDelegate;
        this.OnAnimationEventFired -= DropEvent;

        this.OnAnimationStretchCompleted -= StretchDelegate;
        
    }
}
