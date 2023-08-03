using System;
using UnityEngine;

public class RotateToTargetEuler : MonoBehaviour
{
    [SerializeField] private Vector3 leftEuler;    
    [SerializeField] private Vector3 rightEuler;
    [SerializeField] private float rotationSpeed;

    private Vector3 _targetEuler;

    private void Start()
    {
        _targetEuler = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetEuler), Time.deltaTime * rotationSpeed);
    }

    public void RotateLeft()
    {
        _targetEuler = leftEuler;
    }

    public void RotateRight()
    {
        _targetEuler = rightEuler;
    }
}
