﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    public bool value;
    
    [HideInInspector]
    public bool runTime;
    
}
