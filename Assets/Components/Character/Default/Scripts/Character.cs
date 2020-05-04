using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AillieoUtils.EasyBehaviorTree;

public class Character : MonoBehaviour, IBlackBoardData, IMovable, IHasWaypoints, IClickable, ICanCarry
{
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

    // IHasWaypoints
    [Header("Has Waypoints")]
    [SerializeField]
    private List<Waypoint> _waypoints = null;
    public List<Waypoint> waypoints => _waypoints;

    public Waypoint currentWaypoint { get; set; }
    public Waypoint lastWaypoint { get; set; }

    // IMovable
    public void MoveTo(Vector3 to)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(to);
    }

    public void Stop()
    {
        navMeshAgent.isStopped = true;
    }
    
    // IClickable
    public void OnMouseDown()
    {
        if(!onCooldown)
        {
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
    [Header("Can Carry")]
    [SerializeField]
    private uint _carryingCapacity = 0;
    public uint carryingCapacity { get { return _carryingCapacity; } }

    [SerializeField]
    private List<Transform> _carryingSlots = new List<Transform>();
    public List<Transform> carryingSlots { get { return _carryingSlots; } }

    protected List<ICarriable> _carrying = new List<ICarriable>();
    public List<ICarriable> carrying { get { return _carrying; } }

    public bool Carry(ICarriable carriable)
    {
        if (carriable.isCarried) return false;
        if (carrying.Count >= carryingCapacity) return false;

        bool canCarry = carriable.Carry(this);

        if(canCarry)
        {
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
}
