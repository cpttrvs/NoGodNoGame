using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederStretchingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;
    protected Basket basket;

    [SerializeField]
    private int reasonableAmountOfWeedsToPickUp = 3;

    [Header("State Machine")]
    [SerializeField]
    private string triggerRemainingPickupWork;
    [SerializeField]
    private string triggerRemainingUnplantWork;
    
    [Header("Props")]
    [SerializeField]
    private string basketKey = null;
    [SerializeField]
    private string basketWaypointOnFootKey = null;

    protected override void Init()
    {
        base.Init();

        if (character != null)
        {
            if (character is Weeder)
            {
                weeder = character as Weeder;

                garden = weeder.garden;
                basket = weeder.basket;

                behaviorTree.blackBoard[basketKey] = basket;
                behaviorTree.blackBoard[basketWaypointOnFootKey] = weeder.basketOnFootWaypoint;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        if (garden.GetRemainingWeedsToPickup(weeder.currentGardenWaypointsLane) >= reasonableAmountOfWeedsToPickUp)
        {
            stateAnimator.SetTrigger(triggerRemainingPickupWork);
        } else if (garden.GetRemainingWeedsToUnplant() > 0)
        {
            stateAnimator.SetTrigger(triggerRemainingUnplantWork);
        } else
        {
            Debug.LogWarning("StretchingState: FINISHED nothing more");
        }
    }
}
