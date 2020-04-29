using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarriable
{
    bool isCarried { get; }
    Character isCarriedBy { get; }
    bool Carry(Character c);
    bool Drop();
}
