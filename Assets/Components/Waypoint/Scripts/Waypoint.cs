using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class Waypoint : MonoBehaviour, IBlackBoardData
{
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
