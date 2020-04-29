using System.Collections;
using System.Collections.Generic;
using AillieoUtils.EasyBehaviorTree;
using UnityEngine;

public class CharacterMoveState : CharacterBaseState
{
    [SerializeField]
    private string triggerName = null;

    protected override void Init()
    {
        base.Init();
        if(character != null)
        {
            if (character is IHasWaypoints)
            {
                List<Waypoint> waypoints = character.waypoints;
                Waypoint currentWaypoint = character.currentWaypoint;

                behaviorTree.blackBoard["waypoints"] = new BBList<Waypoint>(waypoints);
                behaviorTree.blackBoard["currentWaypoint"] = currentWaypoint;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        character.currentWaypoint = behaviorTree.blackBoard["currentWaypoint"] as Waypoint;

        stateAnimator.SetTrigger(triggerName);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateExit(animator, animatorStateInfo, layerIndex);

        if(character is IMovable)
        {
            character.Stop();
        }
    }
}
