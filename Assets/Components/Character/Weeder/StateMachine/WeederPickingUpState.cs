using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederPickingUpState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;
    protected Basket basket;

    [Header("State Machine")]
    [SerializeField]
    private string triggerRemainingUnplantWork;
    [SerializeField]
    private string triggerRemainingPickupWork;
    [SerializeField]
    private string triggerOnComplete;

    [Header("Garden")]
    [SerializeField]
    private string gardenKey = null;
    [SerializeField]
    private string gardenBasketWaypointKey = null;
    [SerializeField]
    private string waypointsLanesKey = null;

    [Header("Props")]
    [SerializeField]
    private string basketKey = null;
    [SerializeField]
    private string weederHandsKey = null;

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

                behaviorTree.blackBoard[gardenKey] = garden;
                behaviorTree.blackBoard[gardenBasketWaypointKey] = garden.basketSpot;

                behaviorTree.blackBoard[basketKey] = basket;
                behaviorTree.blackBoard[weederHandsKey] = weeder.handsContainer;

                behaviorTree.blackBoard[waypointsLanesKey] = new BBList<GardenWaypointsLane>(garden.waypointsLanes);

            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        Debug.Log("PickingUpState: FINISHED " + state.ToString());
        if (garden.GetRemainingWeedsToUnplant() > 0 )
        {
            stateAnimator.SetTrigger(triggerRemainingUnplantWork);
        } else if (garden.GetRemainingWeedsToPickup() > 0)
        {
            stateAnimator.SetTrigger(triggerRemainingPickupWork);
        } else
        {
            stateAnimator.SetTrigger(triggerOnComplete);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
