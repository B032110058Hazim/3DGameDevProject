using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        HPHandler hpHandler = other.GetComponent<HPHandler>();
        if (hpHandler != null)
        {
            hpHandler.DiamondCollected();
            gameObject.SetActive(false);
        }
    }       
}