    using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float rotationSpeed = 1f;

    // Neue Variablen für die Kameraführung
    public bool followPlayer = true; // Flag, ob die Kamera dem Spieler folgen soll
    public GameObject puzzleGameObject;
    public float moveSpeed = 5f;     // Geschwindigkeit für die Kamerabewegung

    void Update()
    {
        if (followPlayer)
        {
            // Aktueller Code zum Folgen des Spielers
            Quaternion playerRotation = playerTransform.rotation;
            Vector3 offsetRotated = playerRotation * offset;
            transform.position = playerTransform.position + offsetRotated;
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }
        else
        {
            // Bewege die Kamera zur Zielposition
            transform.position = Vector3.Lerp(transform.position, puzzleGameObject.transform.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, puzzleGameObject.transform.rotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Methode zum Umschalten des Kameraverhaltens
    public void ToggleCameraFollow()
    {
        followPlayer = !followPlayer;
    }
}
