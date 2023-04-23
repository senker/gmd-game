using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    
    private static readonly int Attack01 = Animator.StringToHash("Attack1");
    private static readonly int Attack02 = Animator.StringToHash("Attack2");
    private static readonly int Attack03 = Animator.StringToHash("Attack3");
    private static readonly int Attack04 = Animator.StringToHash("Attack4");

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Attack1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Attack2();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Attack3();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Attack4();
        }
    }



    private void Attack1()
    {
        // Play an attack animation
        animator.SetTrigger(Attack01);
        // Detect enemies in range of attack
        // Apply damage to them
    }
    private void Attack2()
    {
        animator.SetTrigger(Attack02);
    }
    private void Attack3()
    {
        animator.SetTrigger(Attack03);
    }
    private void Attack4()
    {
        animator.SetTrigger(Attack04);
    }

    private void AttackEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }
}
