using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Detection : MonoBehaviour
{
    // Reference to the enemy script that should be activated
    public Enemy enemyScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Activate the enemy script
            enemyScript.enabled = true;
        }
    }
}
