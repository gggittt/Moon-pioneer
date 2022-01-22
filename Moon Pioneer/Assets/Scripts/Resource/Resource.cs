using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public event Action<Resource> Taken;
    public void Pickup()
    {
        Debug.Log($"<color=cyan> {name} picked  </color>");
        Taken?.Invoke(this);
    }
}


