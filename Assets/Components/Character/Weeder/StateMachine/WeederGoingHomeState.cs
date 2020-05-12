using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederGoingHomeState : CharacterBaseState
{
    private Weeder weeder;
    private Basket basket;

    [Header("Props")]
    [SerializeField]
    private string basketKey = null;
    [SerializeField]
    private string weederHandsKey = null;

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
            if (character is Weeder)
            {
                weeder = character as Weeder;
                basket = weeder.basket;

                behaviorTree.blackBoard[basketKey] = basket;
                behaviorTree.blackBoard[weederHandsKey] = weeder.handsContainer;

                behaviorTree.blackBoard[homeDoorWaypointKey] = weeder.homeDoorWaypoint;
                behaviorTree.blackBoard[homeInsideWaypointKey] = weeder.homeInsideWaypoint;
                behaviorTree.blackBoard[homeContainerWaypointKey] = weeder.homeContainerWaypoint;
                
                behaviorTree.debugLogging = false;
            }
        }
    }
}
