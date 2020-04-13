using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AillieoUtils.EasyBehaviorTree;

public class Character : MonoBehaviour, IBlackBoardData, IMovable, IHasWaypoints
{
    [SerializeField]
    private NavMeshAgent navMeshAgent = null;

    [SerializeField]
    private List<Waypoint> _waypoints;
    public List<Waypoint> waypoints => _waypoints;

    public void MoveTo(Vector3 to)
    {
        navMeshAgent.SetDestination(to);
    }

}
