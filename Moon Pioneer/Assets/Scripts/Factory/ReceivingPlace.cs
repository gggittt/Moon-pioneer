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
    private Resource[] _created;

    private void Awake()
    {
        _created = new Resource[_placeCapacity];
        _produceTimeSeconds = new WaitForSeconds(_produceTime);
        _resourceSize = _resourcePrefab.transform.localScale; //localScale lossyScale
    }

    private void Start()
    {
        StartCoroutine(TryCreateResource());
    }

    //private bool HasSpace => _created.Length < _placeCapacity;
    private bool TryGetFreeSpaceIndex(out int resultIndex)
    {
        resultIndex = GetFirstEmptyPlaceIndex();
        return resultIndex != -1;
    }
    
    private bool HasIngredients => true;

    private void ResourceTakenHandler(Resource taken)
    {
        //_created.Remove(taken);
        for (int i = 0; i < _created.Length; i++)
        {
            if (_created[i] == taken)
            {
                _created[i] = null;
            }
        }
        StartCoroutine(TryCreateResource());

        //если взяты сразу 2 реса, не запустится ли она сразу 2 раза?
    }

    private IEnumerator TryCreateResource()
    {
        while (HasIngredients && TryGetFreeSpaceIndex(out int index))
        {
            if (index == 9)
            {
                int a = 3;
            }
                
            int emptyPlaceIndex = GetFirstEmptyPlaceIndex();//или проще индексы хранить в newResource.Index; ? тогда неудобно именно ближайший, первый искать?
            Resource newResource = Instantiate(_resourcePrefab, transform);
            //_created.Add(newResource);
            _created[emptyPlaceIndex] = newResource;
            //все ниже в newResource.Init
            var position = transform.position;
            newResource.transform.position = new Vector3(position.x, position.y, index);
            newResource.name = "Resource " + index;
            newResource.Taken += ResourceTakenHandler;
            _resCount = _created.Length;
            Debug.Log($"<color=cyan> создан рес {index} </color>");
            yield return _produceTimeSeconds;
        }

        Debug.Log($"<color=yellow> stop CreateResource </color>");
    }

    private int GetFirstEmptyPlaceIndex()
    {
        for (int i = 0; i < _created.Length; i++)
        {
            if (_created[i] == null)
            {
                return i;
            }
        }

        return -1;//или возвращать int? => null
    }
    
}