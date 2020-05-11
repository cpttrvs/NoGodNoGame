using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class PeelerIdlingState : CharacterBaseState
{
    protected Peeler peeler;
    protected Basket basketVegetables;
    protected Basket basketPeeled;

    [Header("State Machine")]
    [SerializeField]
    private string triggerPeelingWork = null;

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
                
                behaviorTree.debugLogging = false;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        Debug.Log("Peeler IdlingState: FINISHED " + state.ToString());

        if (basketVegetables.GetContentSize() > 0)
        {
            stateAnimator.SetTrigger(triggerPeelingWork);
        }
        else
        {
            Debug.Log("Peeler IdlingState: FINISHED but still have work");
        }
    }
}
