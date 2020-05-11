using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class PeelerStretchingState : CharacterBaseState
{
    protected Peeler peeler;
    protected Basket basketVegetables;
    
    [Header("State Machine")]
    [SerializeField]
    private string triggerRemainingWork;

    [Header("Props")]
    [SerializeField]
    private string basketKey = null;
    [SerializeField]
    private string basketWaypointOnFootKey = null;
    [SerializeField]
    private string benchWaypointKey = null;

    protected override void Init()
    {
        base.Init();

        if (character != null)
        {
            if (character is Peeler)
            {
                peeler = character as Peeler;
                basketVegetables = peeler.basketVegetables;

                behaviorTree.blackBoard[basketKey] = basketVegetables;
                behaviorTree.blackBoard[basketWaypointOnFootKey] = peeler.basketOnFootWaypoint;

                behaviorTree.blackBoard[benchWaypointKey] = peeler.benchWaypoint;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeStarted(BehaviorTree tree)
    {
        base.BehaviourTree_OnBehaviorTreeStarted(tree);

        Debug.Log("PEELER START STRETCHING");
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        Debug.Log("PEELER STRETCHING RETURN " + state.ToString());
        if(state == BTState.Success)
        {
            if (basketVegetables != null)
            {
                if (basketVegetables.GetContentSize() > 0)
                {
                    Debug.Log("STRETCHING STATE WORK");
                    stateAnimator.SetTrigger(triggerRemainingWork);
                }
                else
                {
                    Debug.Log("Peeler StretchingState: FINISHED but no more work");
                }
            } else
            {
                Debug.Log("Peeler StretchingState: FINISHED but no basket");
            }
        } else
        {

        }
        
    }
}
