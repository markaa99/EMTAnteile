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
    //[SerializeField] public Image[] images;
    [SerializeField] public BPuzzleNumber[] puzzleComponents;
    private int puzzleComponentIndex; 

    public FollowPlayerCamera cameraScript;
    public GameObject movementScript;

    
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with BPuzzle");
        if(cameraScript != null) cameraScript.ToggleCameraFollow();
        // movementScript toggle
        return true;
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
    
    public void Start()
    {
        image.gameObject.SetActive(false);
        puzzleComponentIndex = puzzleComponents.Length - 1;
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
