using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popupText;
    public float displayDuration = 4f;
    private float timer = 0f;
    private bool isDisplaying = false;
    [SerializeField] private bool isTriggerable;
    private bool isShown = false;

    [TextArea]
    public string textContent = "Default Text";

    private void Start()
    {
        popupText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isDisplaying)
        {
            timer += Time.deltaTime;

            if (timer >= displayDuration)
            {
                popupText.gameObject.SetActive(false);
                isDisplaying = false;
                timer = 0f;
            }
        }
    }
    public void ShowText()
    {
        if (!isShown)
        {
            popupText.text = textContent;

            popupText.gameObject.SetActive(true);

            timer = 0f;
            isDisplaying = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTriggerable && !isDisplaying)
        {
            if (other.CompareTag("Player"))
            {
                ShowText();
                isShown = true;
            }
        }
    }
    public void Text1()
    {
        if (!isShown)
        {
            timer = 0f;
            popupText.text = "Mom I'm hooommmeeee!";

            popupText.gameObject.SetActive(true);
            
            isDisplaying = true;
            displayDuration = 2.5f;
        }
    }
    public void Text2()
    {
        if (!isShown)
        {
            timer = 0f;
            popupText.text = "How was school?";

            popupText.gameObject.SetActive(true);
            isDisplaying = true;
            displayDuration = 1.5f;
        }
    }
    public void Text3()
    {
        if (!isShown)
        {
            timer = 0f;
            popupText.text = "It was soooo boring, is it okay if Faith stays over for dinner?";

            popupText.gameObject.SetActive(true);
            isDisplaying = true;
            displayDuration = 4.9f;
        }
    }
    public void Text4()
    {
        if (!isShown)
        {
            timer = 0f;
            popupText.text = "Yes honey, dinner will be served soon I just need to finish some chores.";

            popupText.gameObject.SetActive(true);
            isDisplaying = true;
            displayDuration = 3.9f;
        }
    }
    public void Text5()
    {
        if (!isShown)
        {
            timer = 0f;
            popupText.text = "Ok! Can we watch TV in your room?";

            popupText.gameObject.SetActive(true);
            isDisplaying = true;
            displayDuration = 2.9f;
        }
    }
    public void Text6()
    {
        if (!isShown)
        {
            timer = 0f;
            popupText.text = "Sure. But take your shoes off first!";

            popupText.gameObject.SetActive(true);
            isDisplaying = true;
            displayDuration = 2.9f;
        }
    }
    public void Text7()
    {
        if (!isShown)
        {
            timer = 0f;
            popupText.text = "Yheeessssss mommmm";

            popupText.gameObject.SetActive(true);
            isDisplaying = true;
            displayDuration = 1.5f;
        }
    }
}