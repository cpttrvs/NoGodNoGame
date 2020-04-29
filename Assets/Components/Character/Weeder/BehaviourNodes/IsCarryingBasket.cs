using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IsCarryingBasket : NodeCondition
{
    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if (character != null)
        {
            if(character is Weeder)
            {
                Weeder weeder = character as Weeder;

                return weeder.carrying.Contains(weeder.basket);
            }
        }

        return false;
    }
}
