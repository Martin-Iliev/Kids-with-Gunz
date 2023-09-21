using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubbles : MonoBehaviour
{
    public Transform sponge;
    void Update()
    {
        transform.position = sponge.position;
    }
}
