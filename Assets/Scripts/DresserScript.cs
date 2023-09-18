using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DresserScript : MonoBehaviour
{
    public Transform targetRotation;
    public Transform oldRotation;
    public float rotationSpeed = 90f;
    public float interactionDistance = 15f;
    float positionThreshold = 0.1f;
    private bool isRotating = false;
    private bool isOpen = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera playerCamera = Camera.main;
            if (playerCamera != null ) 
            { 
                Ray ray = playerCamera.ScreenPointToRay(new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject && hit.distance <= interactionDistance && !isRotating)
                {
                    isRotating = true;
                }
            }
            
            
        }
        
        if (isRotating && !isOpen)
        {
            float distance = Vector3.Distance(transform.position, targetRotation.position);

            if (distance > positionThreshold) 
            { 
            float step = rotationSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetRotation.position, step);
            }
            else
            {
                isRotating = false;
                isOpen = true;
            }
        }
        if (isRotating && isOpen)
        {
            float distance = Vector3.Distance(transform.position, oldRotation.position);

            if (distance > positionThreshold)
            {
                float step = rotationSpeed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, oldRotation.position, step);
            }
            else
            {
                isRotating = false;
                isOpen = false;
            }
        }
    }
}
