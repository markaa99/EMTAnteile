using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5f; // Bewegungsgeschwindigkeit
    public float rotationSpeed = 50f; // Rotationsgeschwindigkeit

    private CharacterController controller; // Referenz zum CharacterController

    void Start()
    {
        // Hole die Referenz zum CharacterController
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Bewegung
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Sprung

        // Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
