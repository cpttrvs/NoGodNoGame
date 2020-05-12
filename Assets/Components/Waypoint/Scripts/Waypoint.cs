using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class Waypoint : MonoBehaviour, IBlackBoardData
{
    public Vector3 faceDirection { get { return transform.forward; } }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawLine(transform.position, new Vector3(
            transform.position.x + transform.forward.x,
            transform.position.y + transform.forward.y,
            transform.position.z + transform.forward.z));
    }
}
