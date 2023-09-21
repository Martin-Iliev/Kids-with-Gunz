using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX.Utility;
using UnityEngine.VFX;
using UnityEngine.UIElements;

public class SoapBubbles : MonoBehaviour
{
    public Transform sponge;
    [SerializeField] VisualEffect Bubbles;
    void Update()
    {
        Bubbles.SetVector3("Sponge Pos", sponge.position);
    }
}
