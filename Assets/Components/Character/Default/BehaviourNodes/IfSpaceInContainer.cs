using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IfSpaceInContainer : NodeCondition
{
    [NodeParam]
    private string containerKey = null;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Container container = (Container) behaviorTree.blackBoard[containerKey];

        if(container != null)
        {
            return !container.IsFull();
        }

        return false;
    }
}
