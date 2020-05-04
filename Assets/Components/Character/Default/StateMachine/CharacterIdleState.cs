using System.Collections;
using System.Collections.Generic;
using AillieoUtils.EasyBehaviorTree;
using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{
    [SerializeField]
    protected string triggerName = null;

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        stateAnimator.SetTrigger(triggerName);
    }
}