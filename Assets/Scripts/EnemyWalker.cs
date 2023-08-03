using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWalker : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right
    }

    [SerializeField] private Transform leftTargetTransform;
    [SerializeField] private Transform rightTargetTransform;

    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private float waitTime = 0.5f;

    [Tooltip("For ground detection")]
    [SerializeField] private Transform rayStart;
    [SerializeField] private float maxRayDistance = 300f;
    [SerializeField] private LayerMask layerMask;

    public Direction currentDirection;

    public UnityEvent OnLeftTargetEvent;
    public UnityEvent OnRightTargetEvent;

    private bool _isStopped;

    private void Start()
    {
        leftTargetTransform.parent = null;
        rightTargetTransform.parent = null;
    }

    private void Update()
    {
        if (_isStopped)
        {
            return;
        }

        // walking to the left
        if (currentDirection == Direction.Left)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (transform.position.x < leftTargetTransform.position.x)
            {
                currentDirection = Direction.Right;
                _isStopped = true;
                Invoke(nameof(ContinueWalkAfterWaiting), waitTime);
                OnLeftTargetEvent?.Invoke();
            }
        }
        // walking to the right
        else
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            if (transform.position.x > rightTargetTransform.position.x)
            {
                currentDirection = Direction.Left;
                _isStopped = true;
                Invoke(nameof(ContinueWalkAfterWaiting), waitTime);
                OnRightTargetEvent?.Invoke();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(rayStart.position, Vector3.down, out hit, maxRayDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            transform.position = hit.point;
        }
    }

    private void ContinueWalkAfterWaiting()
    {
        _isStopped = false;
    }
}
