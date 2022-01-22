using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Backpack))]
public class Interacter : MonoBehaviour
{
    public int Test = 1;
    
    [SerializeField] private float _secondsToPrimaryInteract = 0.5f;// initial
    [SerializeField] private float _secondsToRepeatedInteract = 0.2f;//secondary
    private float _stayTime;

    private void OnTriggerStay(Collider other)
    {
        var canInteract = other.TryGetComponent(out IInteractable interactable);
        if (canInteract == false) 
            return;
        
        _stayTime += Time.deltaTime;
        

        if (_stayTime >= _secondsToPrimaryInteract)
        {
            interactable.Interact(this);
            _stayTime = 0;
            Debug.Log($"<color=cyan>Test = {Test}  </color>");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        //если рядом не 1, а 2 Trigger - то баг
        _stayTime = 0;
    }

}


