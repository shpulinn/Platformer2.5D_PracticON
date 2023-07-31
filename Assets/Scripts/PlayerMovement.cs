using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 14;
    [SerializeField] private float friction = 0.6f;
    [SerializeField] private float maxSpeed = 5;

    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private ParticleSystem landingParticles;
    [SerializeField] private AudioSource attackAudio;

    private float _coyoteTime = 0.2f;
    private float _jumpBufferTime = 0.2f;
    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;
    private float _jumpFramesTimer;

    private bool _grounded = true;
    private bool _jumpPressed = false;
    private float _yVelocity;
    private bool _canMove = true;

    private float _normalWalkSpeed = 3f;
    private static readonly int YVelF = Animator.StringToHash("yVel_f");
    private static readonly int Sprint = Animator.StringToHash("Sprint");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int JumpTrig = Animator.StringToHash("Jump");
    private static readonly int Attack = Animator.StringToHash("Attack");

    public static PlayerMovement Instance { get; private set; }

    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameInput.OnJumpAction += GameInputOnOnJumpAction;
    }

    private void GameInputOnOnJumpAction(object sender, EventArgs e)
    {
        Jump();
        _jumpPressed = true;
    }

    private void Update ()
    {
        _jumpFramesTimer += Time.deltaTime;

        if (_grounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;

            _yVelocity = playerRigidbody.velocity.y;
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
        if (Input.GetKeyUp(KeyCode.Space) && playerRigidbody.velocity.x > 0f)
        {
            _coyoteTimeCounter = 0f;
        }

        animator.SetBool(Walk, gameInput.GetMovementDirectionNormalized().x != 0);
        animator.SetBool(Grounded, _grounded);


        //if (EventSystem.current.IsPointerOverGameObject() == false)
        //{
        if (Input.GetMouseButtonDown(0))
        {
            //attackAudio.Play();
            animator.SetTrigger(Attack);
        }
        //}

    }

    private void Jump ()
    {
        //jumpAudio.Play();
        animator.SetTrigger(JumpTrig);
        jumpParticles.Play();
        playerRigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
    }

    private void FixedUpdate ()
    {
        float speedMultiplierInAir = 1f;
        Vector2 input;
        if (_canMove)
        {
            input = gameInput.GetMovementDirectionNormalized();
        } else input = Vector2.zero;

        if (_grounded)
        {
            playerRigidbody.AddForce(input.x * moveSpeed, 0, 0, ForceMode.VelocityChange);
        }
        else
        {
            speedMultiplierInAir = 0.2f;
            if (playerRigidbody.velocity.x > maxSpeed && input.x > 0)
            {
                speedMultiplierInAir = 0;
            }
            if (playerRigidbody.velocity.x < -maxSpeed && input.x < 0)
            {
                speedMultiplierInAir = 0;
            }
            playerRigidbody.AddForce(input.x * moveSpeed * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
        }
        playerRigidbody.AddForce(-playerRigidbody.velocity.x * friction * speedMultiplierInAir, 0, 0, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.CompareTag("Ground") == false)
        {
            return;
        }
        landingParticles.Play();
        animator.SetFloat(YVelF, _yVelocity);
        _yVelocity = 0f;
        if (_grounded)
        {
            return;
        }
        //_canMove = false;
        //Invoke(nameof(AllowMove), .5f);
    }

    private void AllowMove()
    {
        _canMove = true;
    }

    private void OnCollisionStay (Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45f)
            {
                _grounded = true;
            }
        }
    }

    private void OnCollisionExit (Collision collision)
    {
        _grounded = false;
    }

    private void OnDestroy ()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
