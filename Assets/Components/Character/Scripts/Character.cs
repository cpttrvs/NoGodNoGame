using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AillieoUtils.EasyBehaviorTree;

public class Character : MonoBehaviour, IBlackBoardData, IMovable, IHasWaypoints, IClickable
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
    [SerializeField]
    private List<Waypoint> _waypoints = null;
    public List<Waypoint> waypoints => _waypoints;

    public Waypoint currentWaypoint { get; set; }

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
}
