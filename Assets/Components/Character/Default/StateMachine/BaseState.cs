using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using UnityEditor;

public class BaseState : StateMachineBehaviour
{
    [SerializeField]
    private Object behaviorFile = null;

    protected BehaviorTree behaviorTree;
    protected Animator stateAnimator;
    protected GameObject animatedGameobject;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        stateAnimator = animator;
        animatedGameobject = stateAnimator.gameObject;

        behaviorTree = BytesAssetProcessor.LoadBehaviorTree(AssetDatabase.GetAssetPath(behaviorFile));

        if(behaviorTree != null)
        {
            Init();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(behaviorTree != null)
        {
            behaviorTree.Tick(Time.deltaTime);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(behaviorTree != null)
        {
            behaviorTree.OnBehaviorTreeCompleted -= BehaviourTree_OnBehaviorTreeCompleted;
            behaviorTree.OnBehaviorTreeStarted -= BehaviourTree_OnBehaviorTreeStarted;

            behaviorTree.CleanUp();
        }
    }
    
    protected virtual void BehaviourTree_OnBehaviorTreeStarted(BehaviorTree tree)
    {
    }

    protected virtual void BehaviourTree_OnBehaviorTreeCompleted(BehaviorTree tree, BTState state)
    {
        behaviorTree.CleanUp();
    }

    protected virtual void Init()
    {
        behaviorTree.OnBehaviorTreeCompleted -= BehaviourTree_OnBehaviorTreeCompleted;
        behaviorTree.OnBehaviorTreeStarted -= BehaviourTree_OnBehaviorTreeStarted;

        behaviorTree.OnBehaviorTreeCompleted += BehaviourTree_OnBehaviorTreeCompleted;
        behaviorTree.OnBehaviorTreeStarted += BehaviourTree_OnBehaviorTreeStarted;

        behaviorTree.Restart();
    }
}
