using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class TextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popupText;
    public MouseLook stopLook;
    public PlayerController stopMove;
    public RawImage textBox;
    public RawImage Mom;
    public RawImage KidKitchen;
    public RawImage KidComingHome;
    public float delay = 0.1f;
    private bool isDisplaying = false;
    public List<string> stringListStart = new List<string>();
    public List<string> stringListKidKitchen = new List<string>();
    [SerializeField] private bool isTriggerable;
    [SerializeField] private bool firstText;
    private List<string> whatText;
    [SerializeField] TextMeshProUGUI Name;
    private int currentIndex = 0;
    private string kidName;
    private bool talkedKitchen = false;
    public string fullText;
    private string currentText = "";
    public float interactionDistance = 5f;
    [SerializeField] Camera playerCam;

    [TextArea]
    public string textContent = "Default Text";

    private void Start()
    {
        popupText.gameObject.SetActive(false);
        KidKitchen.enabled = false;
        if (firstText)
        { 
            StartCoroutine(ShowText(stringListStart[currentIndex]));
            whatText = stringListStart;
            kidName = "Sophie";
            Name.text = kidName;
            Name.enabled = true;
            KidComingHome.enabled = true;
            Mom.enabled = false;
        }
    }
    public IEnumerator ShowText(string text)
    {
        for (int i = 0; i <= text.Length; i++)
        {
            popupText.gameObject.SetActive(true);
            textBox.enabled = true;
            Name.enabled = true;
            isDisplaying = true;
            currentText = text.Substring(0, i);
            popupText.text = currentText;
            stopLook.enabled = false;
            stopMove._MovementSpeed = 0f;
            yield return new WaitForSeconds(delay);
        }
    }
    private void Update()
    {
        if (isDisplaying && popupText.text == whatText[currentIndex])
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Name.text == "Mom")
                {
                    Name.text = kidName;
                }
                else if (Name.text == kidName)
                {
                    Name.text = "Mom";
                }
                if (firstText)
                {
                    if (Name.text == "Mom")
                    {
                        Mom.enabled = true;
                        KidComingHome.enabled = false;
                    }
                    if (Name.text == "Sophie")
                    {
                        KidComingHome.enabled = true;
                        Mom.enabled = false;
                    }
                }
                if (whatText == stringListKidKitchen)
                {
                    if (Name.text == "Mom")
                    {
                        KidKitchen.enabled = false;
                        Mom.enabled = true;
                    }
                    else if (Name.text == kidName)
                    {
                        KidKitchen.enabled = true;
                        Mom.enabled = false;
                    }
                }
                if (currentIndex == whatText.Count - 1 && !firstText) 
                {
                    popupText.gameObject.SetActive(false);
                    stopLook.enabled = true;
                    stopMove._MovementSpeed = 3f;
                    textBox.enabled = false;
                    Name.enabled = false;
                    isDisplaying = false;
                    Mom.enabled = false;
                    KidKitchen.enabled = false;
                }
                if (currentIndex == whatText.Count - 1 && firstText)
                {
                    popupText.gameObject.SetActive(false);
                    stopLook.enabled = true;
                    stopMove._MovementSpeed = 3f;
                    Name.enabled = false;
                    stopMove.DishGameStart = true;
                    textBox.enabled = false;
                    Mom.enabled = false;
                    KidComingHome.enabled = false;
                    isDisplaying = false;
                    firstText = false;
                }
                popupText.gameObject.SetActive(false);
                
                if (currentIndex != whatText.Count - 1) 
                {
                    currentIndex++;
                    StartCoroutine(ShowText(whatText[currentIndex]));
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && !isDisplaying)
        {
            Camera playerCamera = Camera.main;
            if (playerCamera != null)
            {
                RaycastHit hit;
                bool cast = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance);
                if (cast)
                {
                    if (hit.transform.CompareTag("kidKitchen") && !talkedKitchen)
                    {
                        talkedKitchen = true;
                        currentIndex = 0;
                        firstText = false;
                        whatText = stringListKidKitchen;
                        kidName = "Billy";
                        KidKitchen.enabled = true;
                        Name.text = kidName;
                        StartCoroutine(ShowText(whatText[currentIndex]));
                        Debug.Log("billy text");
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggerable && !isDisplaying)
        {
            if (other.CompareTag("toybox"))
            {
                StartCoroutine(ShowText("Collect all the toys in the toybox!"));
                //isShown = true;
            }
        }
    }
}