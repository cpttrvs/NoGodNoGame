using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IfContainerContains : NodeCondition
{
    [NodeParam]
    private string containerKey = null;
    [NodeParam]
    private int value = 0;
    [Header("Pick one")]
    [NodeParam]
    private bool equals = false;
    [NodeParam]
    private bool more = false;
    [NodeParam]
    private bool less = false;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Container container = (Container)behaviorTree.blackBoard[containerKey];

        int content = container.GetContentSize();
        
        if (equals)
            return content == value;
        if (more)
            return content > value;
        if (less)
            return content < value;

        return false;
    }
}
