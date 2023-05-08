using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    private Rigidbody2D _rb;
    private Collider2D _col;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    private bool isAttacking = false; 
    [SerializeField] private AudioSource enemyDieSoundEffect;

    [SerializeField] private float knockbackForce = 10f;
    private float lastMovement;
    public int maxHealth = 100;
    int _currentHealth;
    public bool flip;
    public float speed;

    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int State = Animator.StringToHash("state");
    private static readonly int Attack01 = Animator.StringToHash("Attack1");
    private static readonly int Attack02 = Animator.StringToHash("Attack2");
    
    
    private enum MovementState { Idle, Running }
    
    private void Update()
    {
        Vector3 scale = transform.localScale;
        MovementState state = MovementState.Idle;

        if (!isAttacking) 
        {
            if (player.transform.position.x > transform.position.x + 0.6f)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                transform.Translate(speed * Time.deltaTime, 0, 0);
                state = MovementState.Running;
            }
            else if (player.transform.position.x < transform.position.x - 0.6f)
            {
                scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
                transform.Translate(speed * Time.deltaTime * -1, 0, 0);
                state = MovementState.Running;
            }
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && !isAttacking)
        {
            // Attack the player
            isAttacking = true; // Set the flag to true before attacking
            if (_currentHealth >= 200)
            {
                animator.SetTrigger(Attack01);
            }
            else
            {
                animator.SetTrigger(Attack02);

            }
            StartCoroutine(AttackPlayer());
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
        animator.SetTrigger(Hurt);
        
        
        if (_currentHealth <= 0)
        {
                Die();
        }
        else
        {
            Vector2 knockbackDirection = transform.position - player.transform.position;
            knockbackDirection.Normalize();
            _rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void Die()
    {
        animator.SetBool(IsDead, true);
        _rb.isKinematic = true;
        enabled = false;
        _col.enabled = false;
        enemyDieSoundEffect.Play();
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    

    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1.0f); // Delay the attack by 1 second

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
            {
                hitCollider.gameObject.GetComponent<PlayerLife>().Die();

                Vector2 knockbackDirection = hitCollider.transform.position - transform.position;
                knockbackDirection.Normalize();
                hitCollider.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 0.1f, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(3.0f); 
        isAttacking = false; 
    }
}


