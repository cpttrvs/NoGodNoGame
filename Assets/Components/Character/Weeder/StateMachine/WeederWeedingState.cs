using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederWeedingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;
    protected Basket basket;

    [Header("Garden")]
    [SerializeField]
    private string gardenKey = null;
    [SerializeField]
    private string gardenEntryKey = null;
    [SerializeField]
    private string gardenBasketKey = null;
    [SerializeField]
    private string waypointsLanesKey = null;

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
                behaviorTree.blackBoard[gardenBasketKey] = garden.basketSpot;
                behaviorTree.blackBoard[basketKey] = basket;
                
                behaviorTree.blackBoard[waypointsLanesKey] = new BBList<WaypointsLane>(garden.waypointsLanes);
                
                behaviorTree.debugLogging = false;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        Debug.Log("FINISH");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
