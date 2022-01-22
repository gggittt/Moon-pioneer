using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int _capacity = 10;
    [SerializeField] private int _bagAmount = 0;
    private List<Resource> _resources;

    private void Awake()
    {
        _resources = new List<Resource>(_capacity);
    }

    public void TryPickResource(Resource resource)
    {
        bool hasSpace = _resources.Count <= _capacity;
        if (hasSpace)
        {
            PickResource(resource);
        }
    }

    private void PickResource(Resource resource)
    {
        resource.Pickup();
        //resource.transform.parent = transform;
        resource.transform.SetParent(transform);
        _resources.Add(resource);
        _bagAmount = _resources.Count;//удали весь _bagAmount
    }
}