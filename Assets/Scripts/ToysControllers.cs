using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToysControllers : MonoBehaviour
{
    private bool isCollected;
    public Object toybox;
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("toybox"))
        { 
            if (!isCollected)
            {
                isCollected = true;
                toybox.GetComponent<ToyCollecting>().ToyCollect();
            }
        }
    }
}
