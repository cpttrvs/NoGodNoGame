using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void MoveTo(Vector3 to);
    void Stop();
}
