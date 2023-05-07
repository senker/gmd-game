using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Detection : MonoBehaviour
{
    // Reference to the enemy script that should be activated
    public Enemy02AI enemyScript;
    private bool _enemyActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player")  && !_enemyActivated)
        {
            // Activate the enemy script
            enemyScript.enabled = true;
            _enemyActivated = true;
        }
    }
}
