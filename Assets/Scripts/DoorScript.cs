using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform targetRotation;
    public Transform oldRotation;
    public float rotationSpeed = 90f;
    private float interactionDistance = 3f;
    private bool isRotating = false;
    private bool isOpen = false;
    public bool finalDoor = false;
    public Object KidShooting;

    public AudioSource open;
    public AudioSource close;
    private void Start()
    {
        open.Play();
        open.Pause();
    }
    private void Update()
    {
        if (finalDoor && KidShooting != null && isOpen)
        {
            KidShooting.GetComponent<KidShooting>().isSwitching = true;
        }
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
            open.UnPause();
            float step = rotationSpeed * Time.deltaTime;

            Quaternion target = Quaternion.Euler(targetRotation.eulerAngles);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, step);

            if (transform.rotation == target)
            {
                open.Play();
                open.Pause();
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
                close.Play();
                isRotating = false;
                isOpen = false;
            }
        }
    }
}
