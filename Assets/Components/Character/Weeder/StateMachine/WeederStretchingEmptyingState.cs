using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederStretchingEmptyingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;
    protected Basket basket;

    [Header("State Machine")]
    [SerializeField]
    private string triggerOnComplete;

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

        Debug.Log("Weeder StretchingEmptyingState: FINISHED " + state.ToString());
        stateAnimator.SetTrigger(triggerOnComplete);
        
    }
}
