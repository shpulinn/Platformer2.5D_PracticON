using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float rotationSpeed;
    private float _xEuler = 90;

    private void Update ()
    {
        float _xAxis = gameInput.GetMovementDirectionNormalized().x;
        if (_xAxis > 0)
        {
            _xEuler = 90f;

        }
        else if (_xAxis < 0)
        {
            _xEuler = 270f;
        }
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, _xEuler, 0), Time.deltaTime * rotationSpeed);
    }
}
