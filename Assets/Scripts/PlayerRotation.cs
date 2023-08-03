using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float rotationSpeed;
    private float _xEuler = 0;

    private void Update ()
    {
        float _xAxis = gameInput.GetMovementDirectionNormalized().x;
        if (_xAxis > 0)
        {
            _xEuler = 0f;

        }
        else if (_xAxis < 0)
        {
            _xEuler = 180f;
        }
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, _xEuler, 0), Time.deltaTime * rotationSpeed);
    }
}
