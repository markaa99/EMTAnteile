using System;
using System.Collections;
using System.Collections.Generic;
using GTC_Scripts;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[5];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;
    private ISelectionBorder _selectionBorder;
    bool interaction = false;
    private void Start()
    {
        
        SerialController serialController = FindObjectOfType<SerialController>();
        if (serialController != null)
        {
            serialController.OnInteractionInput += (interactionInput) =>
            {
                interaction = interactionInput; 
                Debug.Log("InteractoinEvent received with input: " + interactionInput);
            };
        }
    }

    private void Update()
    {
        // Returns the number of Objects found around the given Object
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            _selectionBorder = _colliders[0].GetComponent<ISelectionBorder>();
            
            if (_interactable != null && _selectionBorder != null) 
            {
                _selectionBorder.ShowBorder();
                if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.Interactionprompt);

                if(Keyboard.current.spaceKey.wasPressedThisFrame) _interactable.Interact(this);
            }

        }
        else
        {
            if(_interactable != null) _interactable = null;
            else if(_selectionBorder != null) _selectionBorder.HideBorder();   
            if(_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
