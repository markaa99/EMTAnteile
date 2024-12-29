using System;
using System.Collections;
using System.Collections.Generic;
using GTC_Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BPuzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string Interactionprompt => _prompt;

    private bool number;
    
    
    [SerializeField] public TextMeshProUGUI displayNumber;
    [SerializeField] public Image image;
    [SerializeField] public BPuzzleNumber[] puzzleComponents;
    private int puzzleComponentIndex = 0; 

    public FollowPlayerCamera cameraScript;
    public GameObject movementScript;
    public SerialController serialController;
    public int puzzleNumberIterator;
    
    private bool activated = false;
    
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with BPuzzle");
        if(cameraScript != null) cameraScript.ToggleCameraFollow();
        // movementScript und camerascript toggle
        ToggleCameraFollowAndMovement();
        ActivateCurrentPuzzleComponent();
        activated = true;
        return true;
    }

    public void ToggleCameraFollowAndMovement()
    {
        cameraScript.ToggleCameraFollow();
        serialController.ToggleMovmentAndRotation();
    }
    public void ActivateCurrentPuzzleComponent()
    {
        if (puzzleComponents != null )
        {
            BPuzzleNumber selectedComponent = puzzleComponents[puzzleComponentIndex];
            selectedComponent.ShowBorder();
        }
    }
    public void ChangeNumberPuzzleComponent()
    {
        if (puzzleComponents != null )
        {
            BPuzzleNumber selectedComponent = puzzleComponents[puzzleComponentIndex];
            selectedComponent.ChangeNumber();
            selectedComponent.ShowBorder();
        }
    }
    public void HideBorderOnPuzzleComponent()
    {
        if (puzzleComponents != null)
        {
            BPuzzleNumber selectedComponent = puzzleComponents[puzzleComponentIndex];
            selectedComponent.HideBorder();
        }
    }
    
    void Start()
    {
        //image.gameObject.SetActive(false);
        puzzleComponentIndex = puzzleComponents.Length - 1;
        
        if (activated)
        {
            serialController.OnInteractionInput += (interaction) =>
            {
                if (interaction) ActivateCurrentPuzzleComponent();
            };
            serialController.OnPuzzleInput += ChainReaction;
        }
    }

    void ChainReaction(int index, bool change)
    {
        BPuzzleNumber selectedComponent = puzzleComponents[puzzleComponentIndex];
        selectedComponent.HideBorder();
        if(puzzleComponentIndex == puzzleComponentIndex -2 ) puzzleComponentIndex += index;
        selectedComponent.ShowBorder();
        if(change) selectedComponent.ChangeNumber();
    }
    public void changePuzzleComponentIndexUp()
    {
        if(puzzleComponents == null) return;
        if(puzzleComponentIndex > puzzleComponents.Length -1) puzzleComponentIndex++;
    }    
    public void changePuzzleComponentIndexDown()
    {
        if(puzzleComponents == null) return;
        if(puzzleComponentIndex >= 0) puzzleComponentIndex--;
    }

}
