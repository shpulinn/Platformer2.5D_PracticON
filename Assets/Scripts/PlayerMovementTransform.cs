using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTransform : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private bool isJumping = false;

    private Animator _animator;
    private Rigidbody rb;
    private Vector3 _lastScale = Vector3.one;
    
    private static readonly int YVelF = Animator.StringToHash("yVel_f");
    private static readonly int Sprint = Animator.StringToHash("Sprint");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int JumpTrig = Animator.StringToHash("Jump");
    private float _sprintSpeed = 5f;
    private float _normalWalkSpeed = 2f;
    private float _jumpFramesTimer;
    private bool _grounded;
    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;
    private float _yVelocity;
    private bool _jumpPressed;
    private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        
        gameInput.OnJumpAction += GameInputOnOnJumpAction;
    }
    
    private void GameInputOnOnJumpAction(object sender, EventArgs e)
    {
        if (isJumping)
        {
            return;
        }
        Jump();
        //_jumpPressed = true;
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        _animator.SetTrigger(JumpTrig);
        _animator.SetBool(Grounded, false);
        isJumping = true;
    }

    void Update()
    {
        _jumpFramesTimer += Time.deltaTime;

        if (_grounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;

            _yVelocity = rb.velocity.y;
        }

        if (_jumpPressed)
        {
            _jumpBufferCounter = _jumpBufferTime;
            _jumpPressed = false;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && _jumpFramesTimer > 0.4f)
        {
            _jumpFramesTimer = 0f;
            _jumpBufferCounter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.x > 0f)
        {
            _coyoteTimeCounter = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = _sprintSpeed;
            _animator.SetBool(Sprint, true);
        } else
        {
            moveSpeed = _normalWalkSpeed;
            _animator.SetBool(Sprint, false);
        }
        
        Vector2 input = gameInput.GetMovementDirectionNormalized();
        Vector3 movement = new Vector3(0, 0,input.x);
        //transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (input.x == 1)
        {
            transform.localScale = Vector3.one;
            _lastScale = transform.localScale;
        } else if (input.x == -1)
        {
            transform.localScale = new Vector3(1, 1, -1);
            _lastScale = transform.localScale;
        }
        else
        {
            transform.localScale = _lastScale;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = _sprintSpeed;
            _animator.SetBool(Sprint, true);
        } else
        {
            moveSpeed = _normalWalkSpeed;
            _animator.SetBool(Sprint, false);
        }
        
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // _animator.SetBool(Walk, gameInput.GetMovementDirectionNormalized().x != 0);
        _animator.SetBool(Walk, rb.velocity != Vector3.zero);
        Debug.Log(rb.velocity);
    }

    void OnCollisionEnter(Collision other)
    {
        // Проверка на столкновение с землей
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            _animator.SetBool(Grounded, true);
        }
    }
}
