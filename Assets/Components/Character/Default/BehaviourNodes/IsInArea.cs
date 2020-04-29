using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IsInArea : NodeCondition
{
    [NodeParam]
    private string areaKey = null;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        IBlackBoardData data = behaviorTree.blackBoard[areaKey];

        if(data != null)
        {
            IArea area = data as IArea;

            if (area != null)
            {
                Character character = behaviorTree.blackBoard["self"] as Character;

                if (character != null)
                {
                    return area.AreaContains(character.transform.position);
                }
            }
        }

        return false;
    }
}
