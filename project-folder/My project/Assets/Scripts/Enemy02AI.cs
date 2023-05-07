using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Serialization;

public class Enemy02AI : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    [FormerlySerializedAs("enemyGFX")] public Transform enemyGfx;
    public Animator animator;
    private Collider2D _col;


    [SerializeField] private float knockbackForce = 10f;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public int maxHealth = 100;
    int _currentHealth;
    private Path _path;
    private int _currentWaypoint = 0;
    [SerializeField] private bool _reachedEndOfPath = false;

    
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    
    // Start is called before the first frame update
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
        _currentHealth = maxHealth;
        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void UpdatePath()
    {
        if (_seeker.IsDone())
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
    }
    
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {
        if (_path == null)
            return;

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        } else
        {
            _reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * (speed * Time.deltaTime);
        
        _rb.AddForce(force);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGfx.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGfx.localScale = new Vector3(1f, 1f, 1f);
        }
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
        _rb.isKinematic = true;
        enabled = false;
        _col.enabled = false;
    }
}
