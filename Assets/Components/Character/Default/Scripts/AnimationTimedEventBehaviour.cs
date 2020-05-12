using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTimedEventBehaviour : AnimationCompleteBehaviour
{
    [SerializeField]
    protected List<float> timedEvents = new List<float>();

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        foreach(float f in timedEvents)
        {
            AnimationEvent animEvent = new AnimationEvent();
            animEvent.time = f;
        }
    }
}
