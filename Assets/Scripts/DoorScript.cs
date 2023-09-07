using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform targetRotation;
    public Transform oldRotation;
    public float rotationSpeed = 90f;
    public float interactionDistance = 15f;
    private bool isRotating = false;
    private bool isOpen = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera playerCamera = Camera.main;

            Ray ray = playerCamera.ScreenPointToRay(new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject && hit.distance <= interactionDistance && !isRotating)
            {
                isRotating = true;
            }
        }
        if (isRotating && !isOpen)
        {
            float step = rotationSpeed * Time.deltaTime;

            Quaternion target = Quaternion.Euler(targetRotation.eulerAngles);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);

            if (transform.rotation == target)
            {
                isRotating = false;
                isOpen = true;
            }
        } else if (isRotating && isOpen)
        {
            float step = rotationSpeed * Time.deltaTime;

            Quaternion target = Quaternion.Euler(oldRotation.eulerAngles);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);

            if (transform.rotation == target)
            {
                isRotating = false;
                isOpen = false;
            }
        }
    }
}
