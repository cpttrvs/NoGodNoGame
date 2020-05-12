using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class PeelerGoingHomeState : CharacterBaseState
{
    private Peeler peeler;

    [Header("Props")]
    [SerializeField]
    private string basketVegetablesKey = null;
    [SerializeField]
    private string basketPeeledKey = null;
    [SerializeField]
    private string benchWaypointKey = null;

    [Header("Home")]
    [SerializeField]
    private string homeDoorWaypointKey = null;
    [SerializeField]
    private string homeInsideWaypointKey = null;
    [SerializeField]
    private string homeContainerWaypointKey = null;

    protected override void Init()
    {
        base.Init();

        if (character != null)
        {
            if (character is Peeler)
            {
                peeler = character as Peeler;

                behaviorTree.blackBoard[basketVegetablesKey] = peeler.basketVegetables;
                behaviorTree.blackBoard[basketPeeledKey] = peeler.basketPeeled;
                behaviorTree.blackBoard[benchWaypointKey] = peeler.benchWaypoint;

                behaviorTree.blackBoard[homeDoorWaypointKey] = peeler.homeDoorWaypoint;
                behaviorTree.blackBoard[homeInsideWaypointKey] = peeler.homeInsideWaypoint;
                behaviorTree.blackBoard[homeContainerWaypointKey] = peeler.homeContainerWaypoint;

                behaviorTree.debugLogging = false;
            }
        }
    }
}
