using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseState : BaseState
{
    protected Character character;

    protected override void Init()
    {
        base.Init();

        character = animatedGameobject.GetComponentInChildren<Character>();

        if (character != null)
        {
            behaviorTree.blackBoard["self"] = character;
        }
    }
}
