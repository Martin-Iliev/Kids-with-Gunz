using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ToyCollecting : MonoBehaviour
{
    private int toys;
    [SerializeField] int toyAmount;
    [SerializeField] TextMeshProUGUI ToysText;
    [SerializeField] TextMeshProUGUI ChoreFinishedText;
    public Object textFinish;
    public void ToyCollect()
    {
        toys++;
    }
    private void Update()
    {
        if(toys >= toyAmount)
        {
            
        }
    }
}
