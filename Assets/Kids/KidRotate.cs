using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRotate : MonoBehaviour
{
    void Update()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Player");

        if (targetObject != null)
        {
            Vector3 directionToPlayer = targetObject.transform.position - transform.position;
            directionToPlayer.y = 0f;

            Quaternion lookRotation = Quaternion.LookRotation(-directionToPlayer, Vector3.up);

            transform.rotation = lookRotation;
        }
    }
}
