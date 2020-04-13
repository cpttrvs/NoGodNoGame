using System.Collections;
using System.Collections.Generic;
using AillieoUtils.EasyBehaviorTree;
using UnityEngine;

public struct BBGameObject : IBlackBoardData
{
    public GameObject gameObject;

    public BBGameObject(GameObject g)
    {
        gameObject = g;
    }
}

/*
public struct BBWaypoint : IBlackBoardData
{
    private Waypoint waypoint;

    public BBWaypoint(Waypoint w)
    {
        waypoint = w;
    }
}

public struct BBCharacter : IBlackBoardData
{
    private Character character;

    public BBCharacter(Character c)
    {
        character = c;
    }
}
*/
