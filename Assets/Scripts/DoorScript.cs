using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public Object textCont;
    public TextMeshProUGUI stopText;

    public AudioSource open;
    public AudioSource close;
    private void Start()
    {
        if (stopText != null) { stopText.enabled = false; }
        open.Play();
        open.Pause();
    }
    private void Update()
    {
        if (textCont != null)
        {
            if (finalDoor && isOpen)
            {
                KidShooting.GetComponent<KidShooting>().isSwitching = true;
            }
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
                    if (!finalDoor) { isRotating = true; }
                    if (textCont != null) { 
                        if(finalDoor && !textCont.GetComponent<TextController>().talkedBoyToy || !textCont.GetComponent<TextController>().talkedGirlToy || !textCont.GetComponent<TextController>().talkedKitchen)
                        {
                            StartCoroutine(DoorText());
                        }
                    }
                    if (finalDoor && textCont.GetComponent<TextController>().talkedBoyToy && textCont.GetComponent<TextController>().talkedGirlToy && textCont.GetComponent<TextController>().talkedKitchen)
                    {
                        isRotating = true;
                    }
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
    private IEnumerator DoorText()
    {
        stopText.enabled = true;
        yield return new WaitForSeconds(5.0f);
        stopText.enabled = false;
    }
}
