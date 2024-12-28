﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float rotationSpeed;
    Rigidbody rb;
    [SerializeField]
    bool controllerEnabled;
    private Vector3 input;

    void Start()
    {
        input = new Vector3();
        rb = GetComponent<Rigidbody>();
        SerialController serialController = FindObjectOfType<SerialController>();
        if (serialController != null)
        {
            serialController.OnMovementInput += (controllerInput) =>
            {
                input = controllerInput; 
                //Debug.Log("Event received with input: " + input);
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if (!controllerEnabled)
        {
            // Raw for now because of no smoothing (better for keyboard), for Arduino maybe without raw.
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        }
        Move(input);
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void Move(Vector3 input)
    {
        Vector3 movement = ((transform.forward * -input.x) + (transform.right * input.z)).normalized * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        
    }
}