using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToyCollecting : MonoBehaviour
{
    private int toys;
    [SerializeField] int toyAmount;
    public Object textFinish;
    public void ToyCollect()
    {
        toys++;
    }
    private void Update()
    {
        if(toys >= toyAmount)
        {
            //textFinish.GetComponent<TextController>().StartCoroutine("ShowText");
        }
    }
}
