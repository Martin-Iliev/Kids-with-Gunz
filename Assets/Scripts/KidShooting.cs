using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KidShooting : MonoBehaviour
{
    public Material[] materials;
    private int currentMaterialIndex = 0;
    private Renderer objectRenderer;
    public bool isSwitching = false;
    private bool lastMaterialChanged = false;
    public AudioSource gunshot;
    public RawImage black;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (materials.Length > 0 && objectRenderer != null)
        {
            objectRenderer.material = materials[currentMaterialIndex];
        }
    }

    void Update()
    {
        if (isSwitching && !lastMaterialChanged)
        {
            if (Time.time >= 0.75f * (currentMaterialIndex + 1))
            {
                currentMaterialIndex++;

                if (currentMaterialIndex < materials.Length)
                {
                    objectRenderer.material = materials[currentMaterialIndex];
                }
                else
                {
                    lastMaterialChanged = true;
                }
            }
        }
    }
    private IEnumerator WaitForOneSecond()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("PSA");
    }
    public void StartMaterialSwitch()
    {
        gunshot.Play();
        black.enabled = true;
        StartCoroutine(WaitForOneSecond());
    }
}