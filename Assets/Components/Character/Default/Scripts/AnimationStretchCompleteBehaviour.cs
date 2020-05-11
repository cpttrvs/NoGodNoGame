using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStretchCompleteBehaviour : StateMachineBehaviour
{
    protected Character character;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        character = animator.transform.parent.GetComponentInChildren<Character>();

        if (character == null)
        {
            Debug.LogWarning("AnimationStretchCompleteBehaviour: State Enter, character is null | " + stateInfo.shortNameHash);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (character != null)
        {
            character.OnAnimationStretchComplete(stateInfo.tagHash.ToString());
        }
    }
}
