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
        popupText.text = textContent;

        popupText.gameObject.SetActive(true);

        timer = 0f;
        isDisplaying = true;
    }
}
