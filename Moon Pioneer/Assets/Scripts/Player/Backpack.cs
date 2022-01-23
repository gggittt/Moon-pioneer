using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int _capacity = 10;
    [SerializeField] private BagVisual _bagVisual;
    private List<Resource> _resources;

    public HashSet<ResourceType> GetTypesInStock()
    {
        var result = new HashSet<ResourceType>();
        
        foreach (Resource item in _resources)
        {
            result.Add(item.Type);
        }
        
        return result;
    }

    public void TryPickResource(Resource resource)
    {
        bool hasSpace = _resources.Count <= _capacity;
        if (hasSpace)
        {
            PickResource(resource);
        }
    }

    public void SpendResource(Transform targetPlace, ResourceType resourceType)
    {
        var item = GetResourceFromBug(resourceType);
        _bagVisual.VisualizeRemoving(targetPlace, item);
        _resources.Remove(item);
        item.Delete();
    }

    private void Awake()
    {
        _resources = new List<Resource>(_capacity);
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

    private void PickResource(Resource resource)
    {
        _resources.Add(resource);
        int index = _resources.Count;
        //int index = _resources.FindIndex(a => a == resource);
        resource.Pickup(index);
        //resource.transform.parent = transform;
        
        
        _bagVisual.VisualizeAdd(resource, index);
    }

    
}