using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using UnityEditor;
using System.IO;

public class BaseState : StateMachineBehaviour
{
    [SerializeField]
    private Object behaviorFile = null;
    [SerializeField]
    private string behaviorName = "";

    protected BehaviorTree behaviorTree;
    protected Animator stateAnimator;
    protected GameObject animatedGameobject;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        stateAnimator = animator;
        animatedGameobject = stateAnimator.gameObject;
        
#if UNITY_EDITOR

        behaviorTree = BytesAssetProcessor.LoadBehaviorTree(AssetDatabase.GetAssetPath(behaviorFile));

#else
        string path = "";

        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            path = Path.Combine("StreamingAssets", behaviorName + ".bt");
        } else if(Application.platform == RuntimePlatform.OSXPlayer)
        {
            path = Path.Combine(Application.streamingAssetsPath, behaviorName + ".bt");
        } else if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = Path.Combine(Application.streamingAssetsPath, behaviorName + ".bt");
        } else
        {
            path = Path.Combine(Application.streamingAssetsPath, behaviorName + ".bt");
        }

        behaviorTree = BytesAssetProcessor.LoadBehaviorTree(path);
#endif



        if (behaviorTree != null)
        {
            Init();
        } else
        {
            Debug.LogError(behaviorFile.name + " can't load Behavior Tree from it");
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
