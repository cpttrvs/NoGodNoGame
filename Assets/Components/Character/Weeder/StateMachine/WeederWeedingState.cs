using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeederWeedingState : CharacterBaseState
{
    protected Weeder weeder;
    protected Garden garden;
    protected Basket basket;

    [SerializeField]
    private string gardenKey = null;
    [SerializeField]
    private string gardenEntryKey = null;
    [SerializeField]
    private string basketKey = null;

    protected override void Init()
    {
        base.Init();

        if(character != null)
        {
            if(character is Weeder)
            {
                weeder = character as Weeder;

                garden = weeder.garden;
                basket = weeder.basket;

                behaviorTree.blackBoard[gardenKey] = garden;
                behaviorTree.blackBoard[gardenEntryKey] = garden.entry;
                behaviorTree.blackBoard[basketKey] = basket;
            }
        }
    }
}
