using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SpendingPlace : MonoBehaviour, IInteractable
{
    //[SerializeField] private List<ResourceRequest> _typeAmountPair;
    [SerializeField] private ResourceType _requestedTypes;
    [SerializeField] private int _resourceCapacity = 20;
    [SerializeField] private int _resourceAmount;

    public void TryInteract(Backpack backpack)
    {
        var typesAtBag = backpack.GetTypesInStock();

        bool hasRequestedType = typesAtBag.Contains(_requestedTypes);
        Debug.Log($"<color=cyan> TryInteract с приемкой = {hasRequestedType}  </color>");
        if (hasRequestedType)
        {
            TryTakeResource(backpack, _requestedTypes);
        }
    }
    
    private void TryTakeResource(Backpack backpack, ResourceType resourceType)
    {
        bool isFull = _resourceAmount < _resourceCapacity;
        if (isFull == false)
        {
            TakeResource(backpack, resourceType);
        }
    }

    private void TakeResource(Backpack backpack, ResourceType resourceType)
    {
        backpack.SpendResource(transform, resourceType);
        _resourceAmount++;
        if (_resourceAmount == _resourceCapacity)
        {
            Complete();
        }
    }


    private void Complete()
    {
        Debug.Log($"<color=cyan> цель здания {gameObject.name} готова  </color>");
    }
    
}