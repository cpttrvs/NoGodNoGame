using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AillieoUtils.EasyBehaviorTree;

public class Character : MonoBehaviour, IBlackBoardData, IMovable, IHasWaypoints, IClickable, ICanCarry
{
    [Header("Animations")]
    [SerializeField]
    protected Animator animatorStateMachine = null;
    [SerializeField]
    private string stateWalkBool = null;
    [SerializeField]
    private string stateDropItemTrigger = null;
    [SerializeField]
    private string statePickUpItemTrigger = null;

    [Header("State Machine")]
    [SerializeField]
    private Animator stateMachine = null;
    [SerializeField]
    private string onClickTrigger = null;

    [SerializeField]
    private float cooldownClick = 10f;
    private bool onCooldown = false;

    [Header("Nav Mesh")]
    [SerializeField]
    private NavMeshAgent navMeshAgent = null;

    [Header("Has Waypoints")]
    [SerializeField]
    private List<Waypoint> _waypoints = null;
    public List<Waypoint> waypoints => _waypoints;
    
    [Header("Can Carry")]
    [SerializeField]
    private uint _carryingCapacity = 0;
    public uint carryingCapacity { get { return _carryingCapacity; } }

    [SerializeField]
    private List<Transform> _carryingSlots = new List<Transform>();
    public List<Transform> carryingSlots { get { return _carryingSlots; } }

    // IHasWaypoints
    public Waypoint currentWaypoint { get; set; }
    public Waypoint lastWaypoint { get; set; }

    // IMovable
    public void MoveTo(Vector3 to)
    {
        animatorStateMachine.SetBool(stateWalkBool, true);
        //navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(to);
    }

    public void Stop()
    {
        animatorStateMachine.SetBool(stateWalkBool, false);
        navMeshAgent.isStopped = true;
        //navMeshAgent.enabled = false;
    }
    
    // IClickable
    public void OnMouseDown()
    {
        if(!onCooldown)
        {
            Debug.Log("Character: On Click");
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

    public bool Carry(ICarriable carriable)
    {
        if (carriable.isCarried) return false;
        if (carrying.Count >= carryingCapacity) return false;

        bool canCarry = carriable.Carry(this);

        if(canCarry)
        {
            animatorStateMachine.SetTrigger(statePickUpItemTrigger);

            _carrying.Add(carriable);
            
            if(carriable is MonoBehaviour)
            {
                (carriable as MonoBehaviour).transform.SetParent(carryingSlots[0]);
                (carriable as MonoBehaviour).transform.localPosition = Vector3.zero;
            }
            
            return true;
        }

        return false;
    }

    public bool Drop(ICarriable carriable, Transform pos)
    {
        if (!carriable.isCarried) return false;
        if (carriable.isCarriedBy != this) return false;
        if (!carrying.Contains(carriable)) return false;

        bool canDrop = carriable.Drop();

        if(canDrop)
        {
            animatorStateMachine.SetTrigger(stateDropItemTrigger);

            _carrying.Remove(carriable);

            if (carriable is MonoBehaviour)
            {
                (carriable as MonoBehaviour).transform.SetParent(pos);
                (carriable as MonoBehaviour).transform.localPosition = Vector3.zero;
            }

            return true;
        }

        return false;
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
}
