using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class IsNearObject : NodeCondition
{
    [NodeParam]
    private string objectKey = null;
    [NodeParam]
    private float minimalRange = 1f;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if (character != null)
        {
            MonoBehaviour gameObject = behaviorTree.blackBoard[objectKey] as MonoBehaviour;
            if (gameObject != null)
            {
                float distance = Vector3.Distance(character.transform.position, gameObject.transform.position);

                if (distance <= minimalRange)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
