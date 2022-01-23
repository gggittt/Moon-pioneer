using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;
    [SerializeField] private float _movLerpTime = .6f;

    public int PlaceIndex { get; private set; }
    public event Action<Resource> OnTake;

    public void Init(int index)
    {
        PlaceIndex = index;
        var cashTransform = transform;
        cashTransform.position = new Vector3(cashTransform.position.x, 0, index);
        name = ToString();
        Debug.Log($"<color=green> created {name} </color>");
    }

    public void Pickup(int newIndex)
    {
        PlaceIndex = newIndex;
        Debug.Log($"<color=cyan> {name} picked  </color>");
        OnTake?.Invoke(this);
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

    public void MoveTo(Vector3 localPositionInBag)
    {
        Debug.Log($"<color=cyan> from world{transform.position} to local {localPositionInBag}  </color>");
        StartCoroutine(Move(localPositionInBag));
    }

    private IEnumerator Move(Vector3 localPositionInBag)
    {
        const float tolerance = 0.05f;
        while (Vector3.Distance(localPositionInBag, transform.localPosition) > tolerance)
        {
            Vector3 interpolatedPosition  = Vector3.Lerp(localPositionInBag, transform.localPosition, _movLerpTime);
            
            transform.localPosition = interpolatedPosition ;
            yield return null;
        }
    }
}


