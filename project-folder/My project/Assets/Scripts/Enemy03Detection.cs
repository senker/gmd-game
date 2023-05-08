using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Detection : MonoBehaviour
{
    public Enemy03 enemyScript;
    private bool _enemyActivated = false;
    //public Rigidbody2D target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player")  && !_enemyActivated)
        {
            //target.bodyType = RigidbodyType2D.Dynamic;
            // Activate the enemy script
            enemyScript.enabled = true;
            _enemyActivated = true;
        }
    }
}