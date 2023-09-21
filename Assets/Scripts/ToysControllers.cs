using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToysControllers : MonoBehaviour
{
    private bool isCollected;
    public Object toybox;

    [Header("ToySounds")]
    public AudioSource blockCol;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (blockCol != null) { blockCol.Play(); }
    }
}