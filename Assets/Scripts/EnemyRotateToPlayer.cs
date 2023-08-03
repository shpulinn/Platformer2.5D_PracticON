using Unity.Mathematics;
using UnityEngine;

public class EnemyRotateToPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 leftRotation;
    [SerializeField] private Vector3 rightRotation;
    [SerializeField] private float rotationSpeed = 5f;

    private Transform _playerTransform;
    private Vector3 _targetEuler;

    private void Awake()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (transform.position.x < _playerTransform.position.x)
        {
            _targetEuler = rightRotation;
        }
        else
        {
            _targetEuler = leftRotation;
        }
        
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion.Euler(_targetEuler), rotationSpeed * Time.deltaTime);
    }
}
