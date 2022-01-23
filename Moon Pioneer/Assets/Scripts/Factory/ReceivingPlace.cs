using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivingPlace : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private Vector3 _resourceOffset;

    [SerializeField] private float _produceTime = 0.7f;
    [SerializeField] private Transform _startSpawnPosition;

    [SerializeField] private Vector2Int _gridSize = new Vector2Int(5, 4);
    [SerializeField] int _placeCapacity = 10;

    private Vector3 _resourceSize;

    private WaitForSeconds _produceTimeSeconds;

    //private List<Resource> _created = new List<Resource>(10);
    private Resource[] _resourcesOnPlateau;
    [SerializeField] private bool _isInProcess;

    private void Awake()
    {
        _resourcesOnPlateau = new Resource[_placeCapacity];
        _produceTimeSeconds = new WaitForSeconds(_produceTime);
        _resourceSize = _resourcePrefab.transform.localScale; //localScale lossyScale
    }

    private void Start()
    {
        StartCoroutine(TryCreateResource());
    }

    //private bool HasSpace => _created.Length < _placeCapacity;

    private bool HasIngredients => true;

    private void ResourceOnTakeHandler(Resource taken)
    {
        //_created.Remove(taken);
        //ReleaseTaken(taken);
        int newFreeIndex = taken.PlaceIndex;//удали если не используешь индекс
        _resourcesOnPlateau[newFreeIndex] = null;
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
        for (int i = 0; i < _resourcesOnPlateau.Length; i++)
            if (_resourcesOnPlateau[i] == taken)
                _resourcesOnPlateau[i] = null;
    }

    private IEnumerator TryCreateResource(/*int freeIndex*/)
    {
        while (HasIngredients && TryGetFreeSpaceIndex(out int index))
        {
            _isInProcess = true;

            //todo get from pool
            Resource newResource = Instantiate(_resourcePrefab, transform);
            _resourcesOnPlateau[index] = newResource;
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
        
        for (int i = 0; i < _resourcesOnPlateau.Length; i++)
        {
            if (_resourcesOnPlateau[i] == null)
            {
                resultIndex =  i;
                return true;
            }
        }
        
        return false;//или возвращать int? => null
    }
    
}