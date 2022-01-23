using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResourceRequest
{
    public ResourceType Type; //todo PascalCase?
    public int Amount;
    public bool IsComplete = false;
}

public class SpendingPlace : MonoBehaviour, IInteractable
{
    [SerializeField] private List<ResourceRequest> _typeAmountPair;
    private HashSet<ResourceType> _requestedTypes;

    public void TryInteract(Backpack backpack)
    {
        var availableTypes = backpack.GetAvailableTypes();

        Debug.Log($"<color=cyan> проверь совместимость Intersect HashSet/ List  </color>");
        if (_requestedTypes.Intersect(availableTypes) is List<ResourceType> intersect)
        {
            TrySpend(backpack, intersect[0]);
        }
    }

    private void Awake()
    {
        SetRequestedTypes();
    }

    private void TrySpend(Backpack backpack, ResourceType resourceType)
    {
        ResourceRequest resourceRequest = GetResourceRequestOfType(resourceType);
        //мб убрать эту логику для общего случая, и оставить только частное? а то так грязи много с прогонкой туда-сюда _requestedTypes.Intersect
        //могут быть баги если ГД добавит 2 одинаковых ResourceRequest ?

        bool isComplete = resourceRequest.Amount > 0;
        if (isComplete == false)
        {
            resourceRequest.Amount--;
            if (resourceRequest.Amount == 0)
            {
                resourceRequest.IsComplete = true;
                CheckCompleteCondition();
            }
        }

        backpack.DeleteResource(resourceType);
    }

    private void CheckCompleteCondition()
    {
        foreach (ResourceRequest point in _typeAmountPair)
        {
            if (point.IsComplete == false)
                return;
        }

        Complete();
    }

    private void Complete()
    {
        Debug.Log($"<color=cyan> цель здания {gameObject.name} готова  </color>");
    }

    private ResourceRequest GetResourceRequestOfType(ResourceType resourceType)
    {
        return _typeAmountPair.FirstOrDefault(
            item => item.Type == resourceType);
    }

    private void SetRequestedTypes()
    {
        _requestedTypes = new HashSet<ResourceType>();
        foreach (ResourceRequest item in _typeAmountPair)
        {
            _requestedTypes.Add(item.Type);
        }
    }
}