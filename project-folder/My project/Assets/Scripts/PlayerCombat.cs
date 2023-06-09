using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    [SerializeField] private AudioSource hitSoundEffect;


    private static readonly int Attack01 = Animator.StringToHash("Attack1");
    private static readonly int Attack02 = Animator.StringToHash("Attack2");
    private static readonly int Attack03 = Animator.StringToHash("Attack3");
    private static readonly int Attack04 = Animator.StringToHash("Attack4");
    
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Attack1();
                nextAttackTime = Time.time + 1f / attackRate;
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Attack2();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Attack3();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Attack4();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }



    private void Attack1()
    {
        animator.SetTrigger(Attack01);
        AttackEnemy();
        hitSoundEffect.Play();
    }
    private void Attack2()
    {
        animator.SetTrigger(Attack02);
        AttackEnemy();
        hitSoundEffect.Play();
    }
    private void Attack3()
    {
        animator.SetTrigger(Attack03);
        AttackEnemy();
        hitSoundEffect.Play();
    }
    private void Attack4()
    {
        animator.SetTrigger(Attack04);
        AttackEnemy();
        hitSoundEffect.Play();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void AttackEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }
            Enemy02 enemy02Component = enemy.GetComponent<Enemy02>();
            if (enemy02Component != null)
            {
                enemy02Component.TakeDamage(attackDamage);
            }
            Enemy03 enemy03Component = enemy.GetComponent<Enemy03>();
            if (enemy03Component != null)
            {
                enemy03Component.TakeDamage(attackDamage);
            }
            Boss boss = enemy.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
