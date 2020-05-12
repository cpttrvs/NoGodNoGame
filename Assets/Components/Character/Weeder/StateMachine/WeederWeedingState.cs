using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederWeedingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;
    protected Basket basket;

    [Header("State Machine")]
    [SerializeField]
    private string triggerOnComplete = null;

    [Header("Garden")]
    [SerializeField]
    private string gardenKey = null;
    [SerializeField]
    private string gardenEntryKey = null;
    [SerializeField]
    private string waypointsLanesKey = null;
    [SerializeField]
    private string currentLaneKey = null;

    [Header("Props")]
    [SerializeField]
    private string basketKey = null;

    protected override void Init()
    {
        base.Init();


        if(character != null)
        {
            if(character is Weeder)
            {
                weeder = character as Weeder;

                garden = weeder.garden;
                basket = weeder.basket;

                behaviorTree.blackBoard[gardenKey] = garden;
                behaviorTree.blackBoard[gardenEntryKey] = garden.entry;
                behaviorTree.blackBoard[basketKey] = basket;
                
                behaviorTree.blackBoard[waypointsLanesKey] = new BBList<GardenWaypointsLane>(garden.waypointsLanes);
                behaviorTree.blackBoard[currentLaneKey] = weeder.currentGardenWaypointsLane;
                
                behaviorTree.debugLogging = false;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        if(garden.GetRemainingWeedsToUnplant(weeder.currentGardenWaypointsLane) == 0 &&
            garden.GetRemainingWeedsToPickup(weeder.currentGardenWaypointsLane) == 0 &&
            garden.GetRemainingWeedsToUnplant() > 0)
        {
            //Debug.Log("WeedingState: FINISHED but still have work (lane finished)");

            Init();
        }
        else if (garden.GetRemainingWeedsToUnplant(weeder.currentGardenWaypointsLane) == 0)
        {
            //Debug.Log("WeedingState: FINISHED");

            stateAnimator.SetTrigger(triggerOnComplete);
        } else
        {
            //Debug.Log("WeedingState: FINISHED but still have work");

            Init();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
