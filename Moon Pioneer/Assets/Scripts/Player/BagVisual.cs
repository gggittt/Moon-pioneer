using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagVisual : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetBetweenResources = new Vector3(.2f, .2f, .2f);//получать по из размера префаба и добавлять отступ? да
    [SerializeField] private int _pieceInColumn = 10; //в MP = 10

    public void VisualizeAdd(Resource resource, int index)
    {
        Transform resourceTransform;
        (resourceTransform = resource.transform).SetParent(transform);

        
        
        var localPositionInBag = GetLocalPositionForResource(index, resourceTransform.localScale);
        
        resource.MoveTo(localPositionInBag);
    }
    

    private Vector3 GetLocalPositionForResource(int newResourceIndex, Vector3 localScale)
    {
        int horizontalShiftIndex = newResourceIndex / _pieceInColumn;
        int verticalShiftIndex = newResourceIndex % _pieceInColumn;

        
        var positionX = horizontalShiftIndex * (_offsetBetweenResources.x + localScale.x);
        var positionY = verticalShiftIndex * (_offsetBetweenResources.y + localScale.y);
        
        return new Vector3(positionX, positionY, 0);
    }
    
    private Vector3 GetPositionForResource(int newResourceIndex, Vector3 localScale)
    {
        int horizontalShiftIndex = newResourceIndex / _pieceInColumn;
        int verticalShiftIndex = newResourceIndex % _pieceInColumn;

        Vector3 firstPiecePosition = transform.position;
        
        var positionX = firstPiecePosition.x + horizontalShiftIndex * (_offsetBetweenResources.x + localScale.x);
        var positionY = firstPiecePosition.y + verticalShiftIndex * (_offsetBetweenResources.y + localScale.y);
        
        return new Vector3(positionX, positionY, firstPiecePosition.z);
    }

    private int GetPositionIndexForNewResource(ResourceType resourceType, in List<Resource> resources)
    {
        //они уже отсортированы. идти с конца и получить первый такого же типа
        //некрасиво в куче методом хвостом одинаковые параметры
        //!! пока забить хуй и сделать хоть как-то? 
        //за спиной игрока висит этот Transform BagVisual. = это позиция первого реса
        //при каждом новом ресе стопка идет вверх. когда достигает половины bagCapacity - начинается вторая стопка. 
        //при удалении сдвигается вниз
        //одного типа идут подряд? 
        //Recalculate возьми в Leaderboard
        return 22;
    }
    
    

    public void VisualizeRemoving(Transform targetPlace, Resource resource)
    {
        
    }
}