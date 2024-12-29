using UnityEngine;
using System.IO.Ports;
public class SerialController : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM4", 9600);
    public delegate void MovementInputHandler(Vector3 movementInput);
    public delegate void RotationInputHandler(float rotationInput);
    public delegate void InteractionInputHandler(bool interaction);
    public delegate void PuzzleInputHandler(int puzzleIndexChange, bool interaction);
    public event MovementInputHandler OnMovementInput;
    public event InteractionInputHandler OnInteractionInput;
    public event RotationInputHandler OnRotationInput;
    public event PuzzleInputHandler OnPuzzleInput;
    
    private const int availableButtons = 5;
    private int[] buttonStates = new int[availableButtons];
    
    private Vector3 lastInput = new Vector3();
    private bool lastInputBool = false;
    private float lastRotationInput = 0f;
    private string lastData = "";

    private bool toggleMovementAndRotation = true;
    void Start()
    {
        if (!sp.IsOpen)
            sp.Open();
    }
    void Update()
    {
        if (sp.IsOpen && sp.BytesToRead > 0)
        {
            ReceiveInput();
        }
    }

    void ReceiveInput()
    {
        string data = sp.ReadLine();
        if (data != lastData)
        {
            lastData = data;
            string[] splitData = data.Split(',');
            if (splitData.Length == availableButtons)
            {
                for (int i = 0; i < availableButtons; i++)
                {
                    int.TryParse(splitData[i], out buttonStates[i]);
                }
                ProcessInput();
            }
        }
    }

    void ProcessInput()
    {
        float left = buttonStates[0];
        float right = buttonStates[1];
        float forward = buttonStates[2];
        float backward = buttonStates[3];
        int interactionInt = buttonStates[4];
        
        bool interactionBool = (interactionInt == 1) ? true : false;
        float rotationInput = left - right;
        int puzzleIndexChange = (int)(left - right);
        Vector3 input = new Vector3(forward - backward, 0f, 0f);
        if (toggleMovementAndRotation)
        {
            if (input != lastInput)
            {
                lastInput = input;
                OnMovementInput?.Invoke(input);
            }
            if (rotationInput != lastRotationInput)
            {
                lastRotationInput = rotationInput;
                OnRotationInput?.Invoke(lastRotationInput);
            }
        }
        else if (!toggleMovementAndRotation)
        {
            if (interactionBool != lastInputBool && rotationInput != lastRotationInput)
            {
                lastInputBool = interactionBool;
                lastRotationInput = rotationInput;
                OnPuzzleInput?.Invoke(puzzleIndexChange, interactionBool );
            }
        }
        if (interactionBool != lastInputBool)
        {
            lastInputBool = interactionBool;
            OnInteractionInput?.Invoke(lastInputBool);
        }
    }
    void OnApplicationQuit()
    {
        if (sp.IsOpen)
            sp.Close();
    }

    public void ToggleMovmentAndRotation()
    {
        toggleMovementAndRotation = !toggleMovementAndRotation;
    }
}