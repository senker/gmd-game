using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{

    private float _horizontal;
    private const float Speed = 4f;
    private const float JumpingPower = 14f;
    private bool _isFacingRight = true;

    private Animator _anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private static readonly int State = Animator.StringToHash("state");

    private enum MovementState { Idle, Running, Jumping, Falling }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        UpdateAnimationState();
        Flip();
    }

    private void UpdateAnimationState()
    {
        MovementState state = MovementState.Idle;
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpingPower);
            state = MovementState.Jumping;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            var velocity = rb.velocity;
            velocity = new Vector2(velocity.x, velocity.y * 0.5f);
            rb.velocity = velocity;
            state = MovementState.Falling;
        }

        if (_horizontal == 0f && IsGrounded())
        {
            state = MovementState.Idle;
        }
        else if (IsGrounded())
        {
            state = MovementState.Running;
        }
        else if (rb.velocity.y > .1f)
        {
            state = MovementState.Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Falling;
        }

        _anim.SetInteger(State, (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_horizontal * Speed, rb.velocity.y);
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;
            var transformRef = transform;
            Vector3 localScale = transformRef.localScale;
            localScale.x *= -1f;
            transformRef.localScale = localScale;
        }
    }
}
