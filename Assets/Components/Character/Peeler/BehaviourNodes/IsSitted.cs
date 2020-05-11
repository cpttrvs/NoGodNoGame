using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IsSitted : NodeCondition
{
    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if (character != null)
        {
            if (character is Peeler)
            {
                Peeler peeler = character as Peeler;

                return peeler.isSitted;
            }
        }

        return false;
    }
}
