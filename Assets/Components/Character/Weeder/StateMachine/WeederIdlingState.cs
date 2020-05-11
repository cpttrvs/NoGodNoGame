using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class WeederIdlingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;

    [Header("State Machine")]
    [SerializeField]
    private string triggerUnplantWork = null;
    [SerializeField]
    private string triggerPickupWork = null;

    protected override void Init()
    {
        base.Init();


        if (character != null)
        {
            if (character is Weeder)
            {
                weeder = character as Weeder;

                garden = weeder.garden;

                behaviorTree.debugLogging = false;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        Debug.Log("IdlingState: FINISHED " + state.ToString());

        if (garden.GetRemainingWeedsToUnplant() > 0)
        {
            stateAnimator.SetTrigger(triggerUnplantWork);
        } else if (garden.GetRemainingWeedsToPickup() > 0)
        {
            stateAnimator.SetTrigger(triggerPickupWork);
        } else
        {
            Debug.Log("IdlingState: FINISHED but still have work");
        }
    }
}
