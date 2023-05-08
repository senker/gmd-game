using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    private Rigidbody2D _rb;
    private Collider2D _col;
    public Enemy02AI target;
    
    [SerializeField] private float knockbackForce = 10f;
    private float lastMovement;
    public int maxHealth = 100;
    int _currentHealth;

    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    [SerializeField] private AudioSource enemyDieSoundEffect;

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
        animator.SetTrigger(Hurt);
        
        
        if (_currentHealth <= 0)
        {
                Die();
        }
        else
        {
            // Calculate the knockback direction based on the direction from the player to the enemy
            Vector2 knockbackDirection = transform.position - player.transform.position;

            // Normalize the direction to get a unit vector
            knockbackDirection.Normalize();

            // Apply the knockback force in the calculated direction
            _rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void Die()
    {
        // Die anim
        animator.SetBool(IsDead, true);
        // Disable the enemy
        _rb.bodyType = RigidbodyType2D.Static;
        _rb.gravityScale = 100;
        enabled = false;
        target.enabled = false;
        _col.enabled = false;
        enemyDieSoundEffect.Play();
    }
}
