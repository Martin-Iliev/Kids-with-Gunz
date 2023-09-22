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
        gunshot.Play();
        gunshot.Pause();
        black.enabled = false;
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
            StartCoroutine(WaitForTime(1.5f));
        }
        if (lastMaterialChanged)
        {
            gunshot.UnPause();
            black.enabled = true;
            StartCoroutine(ChangeScene());
        }
    }
    private IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("material changed");
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
    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("PSA");
    }
}