using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class Peel : NodeAction
{
    [NodeParam]
    private string basketVegetablesKey = null;
    [NodeParam]
    private string basketPeeledKey = null;

    private bool actionCompleted = false;
    private bool actionStarted = false;

    public override void Cleanup()
    {
        actionCompleted = false;
        actionStarted = false;
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Peeler peeler = (Peeler)behaviorTree.blackBoard["self"];

        if (peeler != null)
        {
            if (!actionStarted)
            {
                Basket basket = (Basket)behaviorTree.blackBoard[basketVegetablesKey];
                bool success = peeler.PeelAny(basket);

                if(success)
                {
                    actionStarted = true;

                    peeler.OnActionCompleted += Character_OnActionCompleted;

                    return BTState.Running;
                }
            }
            else
            {
                if (!actionCompleted)
                {
                    return BTState.Running;
                }
                else
                {
                    return BTState.Success;
                }
            }
        }


        return BTState.Failure;
    }

    private void Character_OnActionCompleted(Character arg1, bool arg2)
    {
        if (arg2) actionCompleted = true;

        arg1.OnActionCompleted -= Character_OnActionCompleted;
    }
}
