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

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        IBlackBoardData obj = behaviorTree.blackBoard["self"];

        if (obj is ICanCarry)
        {
            ICanCarry canCarry = obj as ICanCarry;

            ICarriable carriable = behaviorTree.blackBoard[objectKey] as ICarriable;

            if (carriable != null)
            {
                bool res = canCarry.Carry(carriable);

                if (res) return BTState.Success;
                else return BTState.Failure;
            }
        }

        return BTState.Failure;
    }
}
