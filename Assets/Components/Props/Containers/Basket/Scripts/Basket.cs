using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : Container, ICarriable
{
    private bool _isCarried;
    public bool isCarried { get { return _isCarried; } }

    private Character _isCarriedBy;
    public Character isCarriedBy { get { return _isCarriedBy; } }
    
    public bool Carry(Character c)
    {
        if (isCarried) return false;

        _isCarriedBy = c;
        _isCarried = true;

        return true;
    }

    public bool Drop()
    {
        if (!isCarried) return false;
        
        _isCarriedBy = null;
        _isCarried = false;

        return true;
    }
}
