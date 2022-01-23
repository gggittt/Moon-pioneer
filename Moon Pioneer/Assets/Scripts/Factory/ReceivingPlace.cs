using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivingPlace : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;

    [SerializeField] private float _produceTime = 0.7f;

    [SerializeField] int _placeCapacity = 10;
    
    private WaitForSeconds _produceTimeSeconds;

    private Resource[] _resourcesInStorage;
    [SerializeField] private bool _isInProcess;

    private void Awake()
    {
        _resourcesInStorage = new Resource[_placeCapacity];
        _produceTimeSeconds = new WaitForSeconds(_produceTime);
    }

    private void Start()
    {
        StartCoroutine(TryCreateResource());
    }

    //private bool HasSpace => _created.Length < _placeCapacity;

    private bool HasIngredients => true;

    private void ResourceOnTakeHandler(Resource takenResource)
    {
        
        int newFreeIndex = takenResource.PlaceIndex; //удали если не используешь индекс //да, здесь в фабрике не используется
        _resourcesInStorage[newFreeIndex] = null;

        TryLaunchFactory();
    }

    private void TryLaunchFactory()
    {
        if (_isInProcess == false)
        {
            StartCoroutine(TryCreateResource());
        }
        else
        {
            Debug.Log($"<color=cyan> уже в процессе  </color>");
        }
    }

    private void ReleaseTaken(Resource taken)
    {
        for (int i = 0; i < _resourcesInStorage.Length; i++)
            if (_resourcesInStorage[i] == taken)
                _resourcesInStorage[i] = null;
    }

    private IEnumerator TryCreateResource(/*int freeIndex*/)
    {
        while (HasIngredients && TryGetFreeSpaceIndex(out int index))
        {
            _isInProcess = true;

            //todo get from pool
            Resource newResource = Instantiate(_resourcePrefab, transform);
            _resourcesInStorage[index] = newResource;
            newResource.Init(index);
            newResource.OnTake += ResourceOnTakeHandler;
            
            yield return _produceTimeSeconds;
        }

        _isInProcess = false;

        Debug.Log($"<color=yellow> закончил CreateResource </color>");
    }

    private bool TryGetFreeSpaceIndex(out int resultIndex)
    {
        resultIndex = -1;
        
        for (int i = 0; i < _resourcesInStorage.Length; i++)
        {
            if (_resourcesInStorage[i] == null)
            {
                resultIndex =  i;
                return true;
            }
        }
        
        return false;//или возвращать nullable "int?" => null
    }
    
}