using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class Crawl : NodeAction
{
    [NodeParam]
    private bool stop = false;

    public override void Cleanup()
    {
    }

    protected override BTState ExecuteTask(float deltaTime)
    {
        Weeder weeder = behaviorTree.blackBoard["self"] as Weeder;

        if (weeder != null)
        {
            if (stop)
                weeder.StopCrawl();
            else
                weeder.Crawl();

            return BTState.Success;
        }

        return BTState.Failure;
    }
}
