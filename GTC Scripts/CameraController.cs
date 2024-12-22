using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Referenz zum Spieler-Objekt
    public GameObject cameraTarget; // Das Objekt, auf das die Kamera fokussiert werden soll
    public Camera mainCamera; // Referenz zur Hauptkamera
    public float smoothSpeed = 5f; // Geschwindigkeit des Smooth Follow

    private bool isLocked = false; // Ist die Kamera gelockt?

    void Update()
    {
        // Überprüfe, ob die Leertaste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Schalte den Lock-Status um
            isLocked = !isLocked;
        }

        if (isLocked)
        {
            // Berechne die Zielrotation (Kamera schaut auf cameraTarget)
            //Quaternion targetRotation = Quaternion.LookRotation(cameraTarget.transform.position - mainCamera.transform.position);
            LockCameraOnTarget();
            // Interpoliere die Kamerarotation
            //mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
        else
        {
            // Kamera folgt der Rotation des Spielers
            transform.rotation = player.transform.rotation;
        }
    }

    void LockCameraOnTarget()
    {
        Vector3 targetPosition = cameraTarget.transform.position;
        targetPosition.x = targetPosition.x - 0.5f;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
