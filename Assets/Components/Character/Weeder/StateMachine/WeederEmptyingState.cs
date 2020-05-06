using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederEmptyingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Basket basket;
    protected Container compostPile;

    [Header("State Machine")]
    [SerializeField]
    private string triggerOnComplete;

    [Header("Props")]
    [SerializeField]
    private string basketKey = null;
    [SerializeField]
    private string compostPileKey = null;

    protected override void Init()
    {
        base.Init();


        if (character != null)
        {
            if (character is Weeder)
            {
                weeder = character as Weeder;
                
                basket = weeder.basket;
                compostPile = weeder.compostPile;
                
                behaviorTree.blackBoard[basketKey] = basket;
                behaviorTree.blackBoard[compostPileKey] = compostPile;

                behaviorTree.debugLogging = false;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        Debug.Log("EmptyingState: FINISHED");

        if (basket.GetCapacity() == 0)
        {
            stateAnimator.SetTrigger(triggerOnComplete);
        }
        else
        {
            Debug.Log("EmptyingState: FINISHED but still have work");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
