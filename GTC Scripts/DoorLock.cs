using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GTC_Scripts;
using TMPro;
using UnityEngine;

public class DoorLock : MonoBehaviour, IInteractable, ISelectionBorder
{
    [SerializeField] private string _prompt;
    public string Interactionprompt => _prompt;

    [SerializeField] TextMeshProUGUI textMeshProUGUI1;
    [SerializeField] TextMeshProUGUI textMeshProUGUI2;
    [SerializeField] TextMeshProUGUI textMeshProUGUI3;
    [SerializeField] TextMeshProUGUI textMeshProUGUI4;

    private int[] numbersForSolution = new int[4];
    public int solutionNumber;
    
    //Door Movement
    public GameObject movingDoor;
    public float moveDuration = 2f;
    bool doorMoved = false;
    public float speed;

    
    public bool Interact(Interactor interactor)
    {
        numbersForSolution[0] = parseTextIntoIntArray(textMeshProUGUI1.text);
        numbersForSolution[1] = parseTextIntoIntArray(textMeshProUGUI2.text);
        numbersForSolution[2] = parseTextIntoIntArray(textMeshProUGUI3.text);
        numbersForSolution[3] = parseTextIntoIntArray(textMeshProUGUI4.text);

        if(calcNumberForDoorCheck(numbersForSolution)) { 
            Debug.Log("Right Numbers");
            if (!doorMoved)
            {
                doorMoved = true;
                StartCoroutine(MoveObjectToTarget());
            }
        }
        else Debug.Log("Wrong Numbers");
        return true;
    }

    bool calcNumberForDoorCheck(int[] binaryArray)
    {
        int result = 0;
        int power = 0;

        for (int i = binaryArray.Length - 1; i >= 0; i--)
        {
            if (binaryArray[i] == 1)
            {
                result += (int)Math.Pow(2, power);
            }
            power++;
        }
        return result == solutionNumber;

    }

    int parseTextIntoIntArray(string text)
    {
        try
        {
            int n1 = int.Parse(text);
            return n1;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
        return -1;
    }


    IEnumerator MoveObjectToTarget()
    {
        float timeElapsed = 0f;
        Vector3 startPosition = movingDoor.transform.position;
        Vector3 targetPosition = movingDoor.transform.position;
        targetPosition.z = movingDoor.transform.position.z + 2f;

        while (timeElapsed < moveDuration)
        {
            movingDoor.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        movingDoor.transform.position = targetPosition; // Sicherstellen, dass die Zielposition erreicht wird
        
    } 
    public void ShowBorder() {}
    public void HideBorder() {}
}
