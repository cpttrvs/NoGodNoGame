using UnityEngine;

public interface IArea
{
    Collider area { get; }
    bool AreaContains(Vector3 pos);
}
