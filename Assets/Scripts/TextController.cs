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
        if (!isShown) { 
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
}
