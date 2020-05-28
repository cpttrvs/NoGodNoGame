using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class CarryObject : NodeAction
{
    [NodeParam]
    private string objectKey = null;

    private bool actionCompleted = false;
    private bool actionStarted = false;

    public override void Cleanup()
    {
        actionCompleted = false;
        actionStarted = false;
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if (character is ICanCarry)
        {
            if(!actionStarted)
            {
                ICanCarry canCarry = character as ICanCarry;

                ICarriable carriable = behaviorTree.blackBoard[objectKey] as ICarriable;

                if (carriable != null)
                {
                    bool res = canCarry.Carry(carriable);

                    if (res)
                    {
                        actionStarted = true;

                        character.OnActionCompleted += Character_OnActionCompleted;

                        return BTState.Running;
                    }
                }
            } else
            {
                if(!actionCompleted)
                {
                    return BTState.Running;
                } else
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
