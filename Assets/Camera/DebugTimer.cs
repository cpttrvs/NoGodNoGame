﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTimer : MonoBehaviour
{
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }
}
