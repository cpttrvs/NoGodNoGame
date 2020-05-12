using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTimedEventBehaviour : AnimationCompleteBehaviour
{
    [System.Serializable]
    protected class TimedEvent
    {
        public float timer = 0f;
        [HideInInspector]
        public bool fired = false;
    }

    [SerializeField]
    private float delta = 0.05f;
    [SerializeField]
    protected List<TimedEvent> timedEvents = new List<TimedEvent>();

    private float timer = 0;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        timer = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        timer += Time.deltaTime;

        foreach (TimedEvent te in timedEvents)
        {
            if(!te.fired)
            {
                float f = te.timer;

                if (timer >= f - delta && timer < f + delta)
                {
                    te.fired = true;
                    Debug.Log(character.name + " EVENT " + f + " timer: " + timer);
                    character.OnAnimationEvent(stateInfo.tagHash.ToString());
                }
            }
        }

    }
}
