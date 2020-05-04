using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class Area : MonoBehaviour, IBlackBoardData
{
    [SerializeField]
    protected Collider _area = null;
    public Collider area { get { return _area; } }
    public bool AreaContains(Vector3 pos)
    {
        return area.bounds.Contains(pos);
    }
}
