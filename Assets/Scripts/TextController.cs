using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popupText;
    public MouseLook stopLook;
    public PlayerController stopMove;
    public RawImage textBox;
    public float delay = 0.1f;
    public float displayDuration = 4f;
    private float timer = 0f;
    private bool isDisplaying = false;
    public List<string> stringList = new List<string>();
    [SerializeField] private bool isTriggerable;
    [SerializeField] private bool firstText;
    private int currentIndex = 0;
    private bool isShown = false;
    public string fullText;
    private string currentText = "";

    [TextArea]
    public string textContent = "Default Text";

    private void Start()
    {
        popupText.gameObject.SetActive(false);
        if (firstText)
        { 
            StartCoroutine(ShowText(stringList[currentIndex]));
        }
    }
    public IEnumerator ShowText(string text)
    {
        for (int i = 0; i <= text.Length; i++)
        {
            popupText.gameObject.SetActive(true);
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
        if (isDisplaying && popupText.text == stringList[currentIndex])
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentIndex == stringList.Count - 1 && !firstText) 
                {
                    enabled = false;
                    popupText.gameObject.SetActive(false);
                    stopLook.enabled = true;
                    stopMove._MovementSpeed = 3f;
                    textBox.enabled = false;
                }
                if (currentIndex == stringList.Count - 1 && firstText)
                {
                    enabled = false;
                    popupText.gameObject.SetActive(false);
                    stopLook.enabled = true;
                    stopMove._MovementSpeed = 3f;
                    stopMove.DishGameStart = true;
                    textBox.enabled = false;
                }
                popupText.gameObject.SetActive(false);
                isDisplaying = false;
                if (currentIndex != stringList.Count - 1) 
                {
                    currentIndex++;
                    StartCoroutine(ShowText(stringList[currentIndex]));
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
                isShown = true;
            }
        }
    }
}