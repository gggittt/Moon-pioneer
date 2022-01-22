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
    [SerializeField] int _resCount = 0;

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

    private void ResourceTakenHandler(Resource taken)
    {
        //_created.Remove(taken);
        //ReleaseTaken(taken);
        int newFreeIndex = taken.PlaceIndex;
        _resourcesOnPlateau[newFreeIndex] = null;
        //fixme
        if (_isInProcess == false)
        {
            StartCoroutine(TryCreateResource());
        }
        else
        {
            Debug.Log($"<color=cyan> уже в процессе  </color>");
        }

        //если взяты сразу 2 реса, не запустится ли она сразу 2 раза?
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

            //или проще индексы хранить в newResource.Index; ? тогда неудобно именно ближайший, первый искать?
            Resource newResource = Instantiate(_resourcePrefab, transform);
            //_created.Add(newResource);
            _resourcesOnPlateau[index] = newResource;
            //все ниже в newResource.Init
            var position = transform.position;
            newResource.transform.position = new Vector3(position.x, 0, index);
            newResource.name = "Resource " + index;
            newResource.Taken += ResourceTakenHandler;
            _resCount = _resourcesOnPlateau.Length;
            Debug.Log($"<color=cyan> создан рес {index} </color>");
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