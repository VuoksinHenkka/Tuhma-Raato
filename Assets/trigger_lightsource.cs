using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trigger_lightsource : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) GameManager.Instance.ModifyPlayersLightAmount(1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) GameManager.Instance.ModifyPlayersLightAmount(-1);
    }
}
