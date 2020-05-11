using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IsCarryingBasket : NodeCondition
{
    [NodeParam]
    private string basketKey = null;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if (character != null)
        {
            Basket basket = behaviorTree.blackBoard[basketKey] as Basket;

            return character.carrying.Contains(basket);
        }

        return false;
    }
}
