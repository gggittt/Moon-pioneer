using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    
    [SerializeField] private Backpack _backpack;
    
    
    [SerializeField] private float _secondsToPrimaryInteract = 0.5f;// initial
    [SerializeField] private float _secondsToRepeatedInteract = 0.2f;//secondary
    private float _stayTime;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resource))
        {
            _backpack.TryPickResource(resource);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        bool canInteract = other.TryGetComponent(out IInteractable interactable);
        if (canInteract == false) 
            return;
        
        _stayTime += Time.deltaTime;
        

        if (_stayTime >= _secondsToPrimaryInteract)
        {
            interactable.TryInteract(_backpack);
            _stayTime = 0;
            Debug.Log($"<color=cyan>Test =   </color>");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        //если рядом не 1, а 2 Trigger - то баг
        _stayTime = 0;
    }
    
}


