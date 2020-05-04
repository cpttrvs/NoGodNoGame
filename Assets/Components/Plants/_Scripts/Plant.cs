using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IContainable
{
    [SerializeField]
    private bool _isPlanted = false;
    public bool isPlanted { get { return _isPlanted; } }

    public bool Unplant()
    {
        if(isPlanted)
        {
            _isPlanted = false;
            return true;
        }

        return false;
    }


    // IContainable
    public bool isContained { get; set; }
    public Container container { get; set; }
    
}
