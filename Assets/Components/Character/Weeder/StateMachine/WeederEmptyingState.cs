﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederEmptyingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;
    protected Basket basket;
    protected CompostPile compostPile;

    [Header("State Machine")]
    [SerializeField]
    private string triggerOnComplete;

    [Header("Garden")]
    [SerializeField]
    private string gardenKey = null;
    [SerializeField]
    private string gardenEntryKey = null;

    [Header("Props")]
    [SerializeField]
    private string basketKey = null;
    [SerializeField]
    private string compostPileKey = null;
    [SerializeField]
    private string compostPileWaypointKey = null;

    protected override void Init()
    {
        base.Init();


        if (character != null)
        {
            if (character is Weeder)
            {
                weeder = character as Weeder;
                
                basket = weeder.basket;
                garden = weeder.garden;
                compostPile = weeder.compostPile;

                behaviorTree.blackBoard[gardenKey] = garden;
                behaviorTree.blackBoard[gardenEntryKey] = garden.entry;

                behaviorTree.blackBoard[basketKey] = basket;
                behaviorTree.blackBoard[compostPileKey] = compostPile;
                behaviorTree.blackBoard[compostPileWaypointKey] = compostPile.waypoint;

                behaviorTree.debugLogging = false;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        Debug.Log("EmptyingState: FINISHED");

        if (basket.GetContentSize() > 0)
        {
            Debug.LogWarning("EmptyingState: FINISHED but basket is not empty");
        }

        stateAnimator.SetTrigger(triggerOnComplete);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
