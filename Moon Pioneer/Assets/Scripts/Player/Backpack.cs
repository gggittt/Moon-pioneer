using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int _capacity = 10;
    [SerializeField] private int _bagAmount = 0;
    [SerializeField] private BagVisual _bag;
    private List<Resource> _resources;

    public HashSet<ResourceType> GetAvailableTypes()
    {
        var result = new HashSet<ResourceType>();
        
        foreach (Resource item in _resources)
        {
            result.Add(item.Type);
        }
        
        return result;
    }

    public void DeleteResource(ResourceType resourceType)
    {
        var item = GetResourceFromBug(resourceType);
        _resources.Remove(item);
        item.Delete();
    }

    private Resource GetResourceFromBug(ResourceType resourceType)
    {
        foreach (Resource item in _resources)
        {
            if (item.Type == resourceType)
            {
                return item;
            }
        }
        return null;
    }

    public void TryPickResource(Resource resource)
    {
        bool hasSpace = _resources.Count <= _capacity;
        if (hasSpace)
        {
            PickResource(resource);
        }
    }

    private void Awake()
    {
        _resources = new List<Resource>(_capacity);
    }

    private void PickResource(Resource resource)
    {
        resource.Pickup();
        //resource.transform.parent = transform;
        resource.transform.SetParent(transform);
        _bag.Visualize(resource);
        _resources.Add(resource);
        _bagAmount = _resources.Count;//удали весь _bagAmount
    }

    
}