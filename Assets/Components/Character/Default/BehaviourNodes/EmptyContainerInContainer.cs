using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class EmptyContainerInContainer : NodeAction
{
    [NodeParam]
    private string fromContainerKey = null;
    [NodeParam]
    private string toContainerKey = null;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if(character != null)
        {
            Container from = behaviorTree.blackBoard[fromContainerKey] as Container;
            Container to = behaviorTree.blackBoard[toContainerKey] as Container;

            if(character.EmptyContainerInContainer(from, to))
            {
                Debug.Log("EMPTY " + from.name + " -> " + to.name);
                return BTState.Success;
            }
        }

        return BTState.Failure;
    }
}
