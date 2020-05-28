using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class MoveToObject : NodeAction
{
    [NodeParam]
    private string objectKey = null;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        IBlackBoardData obj = behaviorTree.blackBoard["self"];

        if (obj is IMovable)
        {
            IMovable movable = obj as IMovable;

            MonoBehaviour gameObject = behaviorTree.blackBoard[objectKey] as MonoBehaviour;
            
            if (gameObject != null)
            {
                movable.MoveTo(gameObject.transform.position);

                return BTState.Success;
            }
        }

        return BTState.Failure;
    }
}
