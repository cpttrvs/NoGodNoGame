using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class PeelerPeelingState : CharacterBaseState
{
    protected Peeler peeler;
    protected Basket basketVegetables;
    protected Basket basketPeeled;

    [Header("State Machine")]
    [SerializeField]
    private string triggerOnComplete;

    [Header("Props")]
    [SerializeField]
    private string benchWaypointKey = null;
    [SerializeField]
    private string basketVegetablesKey = null;
    [SerializeField]
    private string basketVegetablesWaypointKey = null;
    [SerializeField]
    private string basketPeeledKey = null;
    [SerializeField]
    private string basketPeeledWaypointKey = null;

    protected override void Init()
    {
        base.Init();


        if (character != null)
        {
            if (character is Peeler)
            {
                peeler = character as Peeler;

                basketVegetables = peeler.basketVegetables;
                basketPeeled = peeler.basketPeeled;

                behaviorTree.blackBoard[basketVegetablesKey] = basketVegetables;
                behaviorTree.blackBoard[basketVegetablesWaypointKey] = peeler.basketVegetablesWaypoint;

                behaviorTree.blackBoard[basketPeeledKey] = basketPeeled;
                behaviorTree.blackBoard[basketPeeledWaypointKey] = peeler.basketPeeledWaypoint;

                behaviorTree.blackBoard[benchWaypointKey] = peeler.benchWaypoint;

                behaviorTree.debugLogging = false;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        if(basketVegetables.GetContentSize() > 0)
        {
            Init();
        } else
        {
            Debug.Log("Peeling State: FINISHED " + state.ToString());
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);
    }
}
