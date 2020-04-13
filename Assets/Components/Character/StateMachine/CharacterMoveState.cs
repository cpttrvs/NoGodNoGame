using System.Collections;
using System.Collections.Generic;
using AillieoUtils.EasyBehaviorTree;
using UnityEngine;

public class CharacterMoveState : BaseState
{
    [SerializeField]
    private string triggerName = null;

    private Character character;

    protected override void Init()
    {
        base.Init();

        character = animatedGameobject.GetComponentInChildren<Character>();

        if(character != null)
        {
            behaviorTree.blackBoard["self"] = character;

            if (character is IHasWaypoints)
            {
                List<Waypoint> waypoints = character.waypoints;

                behaviorTree.blackBoard["waypoints"] = new BBList<Waypoint>(waypoints);
                behaviorTree.blackBoard["currentWaypoint"] = null;
            }
        }
    }

    protected override void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        base.BehaviourTree_OnBehaviorTreeCompleted(tree, state);

        stateAnimator.SetTrigger(triggerName);
    }
}
