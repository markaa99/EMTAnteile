using System;
using System.Collections;
using System.Collections.Generic;
using GTC_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BPuzzle : MonoBehaviour, IInteractable, ISelectionBorder
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
        if(displayNumber != null) changeNumber();
        if(cameraScript != null) cameraScript.ToggleCameraFollow();
        return true;
    }

    public void ChoseComponent()
    {
        int i = puzzleComponents.Length - 1;
        if (puzzleComponents != null)
        {
            BPuzzleNumber selectedComponent = puzzleComponents[i];
            
        }
    }

    public int changeNumber()
    {
        string n1 = "1";
        string n2 = "0";
        if (!number)
        {
            displayNumber.text = n1;
            number = true;
            return 1;
        }
        else
        {
            displayNumber.text = n2;
            number = false;
            return 0;
        }
    }


    public void Start()
    {
        image.gameObject.SetActive(false);
    }

    public void ShowBorder()
    {
        image.transform.position = displayNumber.transform.position;
        image.gameObject.SetActive(true);
    }

    public void HideBorder()
    {
        image.gameObject.SetActive(false);
    }
    
}
