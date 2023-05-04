using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    private Rigidbody2D _rb;
    private Collider2D _col;
    
    private float lastMovement;
    public int maxHealth = 100;
    int _currentHealth;
    public bool flip;
    public float speed;

    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int State = Animator.StringToHash("state");
    
    private enum MovementState { Idle, Running }
    
    private void Update()
    {
        Vector3 scale = transform.localScale;
        MovementState state = MovementState.Idle;
    
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime, 0, 0);
            state = MovementState.Running;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
            state = MovementState.Running;
        }

        animator.SetInteger(State, (int)state);
        transform.localScale = scale;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        // Play hurt animation
        
        animator.SetTrigger(Hurt);
        
        if (_currentHealth <= 0)
        {
                Die();
        }
    }

    private void Die()
    {
        // Die anim
        animator.SetBool(IsDead, true);
        // Disable the enemy
        _rb.isKinematic = true;
        enabled = false;

        _col.enabled = false;

        
    }
}
