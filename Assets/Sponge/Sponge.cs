using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Sponge : MonoBehaviour
{
    
    Vector3 pos;
    public float offset = 3f;

    public Camera DishCam;
    public Transform _sponge;
    
    private void Update()
    {
        
            pos = Input.mousePosition;
            pos.z = offset;
            _sponge.transform.position = Camera.main.ScreenToWorldPoint(pos);
             
          
    }
}
