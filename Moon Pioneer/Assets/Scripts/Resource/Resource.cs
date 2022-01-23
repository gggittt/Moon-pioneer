using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;

    public int PlaceIndex { get; set; }
    public event Action<Resource> OnTake;
    public void Pickup()
    {
        Debug.Log($"<color=cyan> {name} picked  </color>");
        OnTake?.Invoke(this);
    }

    public void Init(int index)
    {
        PlaceIndex = index;
        var cashTransform = transform;
        cashTransform.position = new Vector3(cashTransform.position.x, 0, index);
        name = ToString();
        Debug.Log($"<color=green> created {name} </color>");
    }

    public override string ToString()
    {
        return "Resource " + PlaceIndex;
    }

    public void Delete()
    {
        Destroy(gameObject);
        //todo ToPoll();
    }
}


