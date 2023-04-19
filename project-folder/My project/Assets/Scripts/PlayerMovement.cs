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
    
    private enum MovementState { Idle, Running, Jumping, Falling }
    
    private static readonly int Running = Animator.StringToHash("Running");

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            var velocity = rb.velocity;
            velocity = new Vector2(velocity.x, velocity.y * 0.5f);
            rb.velocity = velocity;
        }

        _anim.SetBool(Running, _horizontal is > 0f or < 0f);

        Flip();
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
