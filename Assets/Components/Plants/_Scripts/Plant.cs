using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IContainable
{
    [SerializeField]
    private bool _isPlanted = false;
    public bool isPlanted { get { return _isPlanted; } }



    // IContainable
    public bool isContained { get; set; }
    public Container container { get; set; }
    
}
