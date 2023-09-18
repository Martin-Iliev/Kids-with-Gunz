using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlateCleaning : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public Material Dirt;
    public float transparency = 2;
    public float cleaningSpeed = 0.01f;

    public Camera player;
    public Camera dishCam;
    public GameObject dishAssets;

    float mouseVeclocity;
    float oldMouseX;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Dirt != null && dishCam != null) 
        {
            mouseVeclocity = oldMouseX - Input.mousePosition.x;
            oldMouseX = Input.mousePosition.x;

            Debug.Log(mouseVeclocity);

            Vector4 colour = new Vector4(1f, 1f, 1f, transparency);
            Dirt.color = colour;
            if (transparency > 0f && mouseVeclocity > 10)
            {
                transparency -= cleaningSpeed;
            }

            if (transparency <= 0)
            {
                Completed();
            }
        }
        
    }

    void Completed()
    {
        player.enabled = true;
        dishCam.enabled = false;
        dishAssets.SetActive(false);
    }
}
