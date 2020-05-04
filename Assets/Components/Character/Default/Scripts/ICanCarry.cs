using System.Collections.Generic;
using UnityEngine;

public interface ICanCarry
{
    uint carryingCapacity { get; }

    List<ICarriable> carrying { get; }

    List<Transform> carryingSlots { get; }

    bool Carry(ICarriable carriable);
    bool Drop(ICarriable carriable, Transform pos);
}
