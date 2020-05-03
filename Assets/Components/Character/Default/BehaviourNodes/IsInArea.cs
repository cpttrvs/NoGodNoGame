using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

[System.Serializable]
public class IsInArea : NodeCondition
{
    [NodeParam]
    private string areaKey = null;

    [NodeParam]
    private bool multipleAreas = false;

    [NodeParam]
    private bool anyArea = false;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        List<Area> data = new List<Area>();

        if(multipleAreas)
        {
            //!!add other types
            List<WaypointsLane> temp = ((BBList<WaypointsLane>)behaviorTree.blackBoard[areaKey]).list;
            foreach (WaypointsLane wl in temp)
                data.Add(wl);
        } else
        {
            data.Add(behaviorTree.blackBoard[areaKey] as Area);
        }

        if(data.Count > 0)
        {
            Character character = behaviorTree.blackBoard["self"] as Character;

            if (character != null)
            {
                if(anyArea)
                {
                    foreach (Area area in data)
                    {
                        if (area.AreaContains(character.transform.position))
                            return true;
                    }
                } else
                {
                    foreach(Area area in data)
                    {
                        if (!area.AreaContains(character.transform.position))
                            return false;
                    }

                    return true;
                }
            }
            
        }

        return false;
    }
}
