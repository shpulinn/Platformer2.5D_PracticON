using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera activeCameraInTriggerZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            activeCameraInTriggerZone.Priority = 100;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            activeCameraInTriggerZone.Priority = 1;
        }
    }
}
