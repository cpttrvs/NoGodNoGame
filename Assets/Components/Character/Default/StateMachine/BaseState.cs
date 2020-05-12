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

    protected BehaviorTree behaviorTree;
    protected Animator stateAnimator;
    protected GameObject animatedGameobject;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        stateAnimator = animator;
        animatedGameobject = stateAnimator.gameObject;

#if UNITY_WEBGL
        //Debug.Log(Application.persistentDataPath + "/Assets/StreamingAssets/" + behaviorFile.name + ".bt");
        //string filepath = System.IO.Path.Combine(Application.persistentDataPath + "/Assets/StreamingAssets/", behaviorFile.name + ".bt");

        /*
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] allFiles = directoryInfo.GetFiles(behaviorFile.name + ".bt");

        Debug.Log(Application.streamingAssetsPath + " " + allFiles.Length);

        if (allFiles.Length == 0)
            Debug.LogError("No files at : " + Application.streamingAssetsPath);
    
        behaviorTree = BytesAssetProcessor.LoadBehaviorTree(allFiles[0].FullName);
        */
        
        behaviorTree = BytesAssetProcessor.LoadBehaviorTree("Assets/Resources/" + behaviorFile.name + ".bt");
#else
        behaviorTree = BytesAssetProcessor.LoadBehaviorTree(AssetDatabase.GetAssetPath(behaviorFile));
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
