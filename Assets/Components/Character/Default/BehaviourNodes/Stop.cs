using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class Stop : NodeActionWait
{
    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        IBlackBoardData obj = behaviorTree.blackBoard["self"];

        if (obj is IMovable)
        {
            IMovable movable = obj as IMovable;

            movable.Stop();
        }

        return base.ExecuteTask(deltaTime);
    }
}
