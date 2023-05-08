using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Detection : MonoBehaviour
{
    public Rigidbody2D target;
    private bool _enemyActivated = false;
    //public Rigidbody2D target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player")  && !_enemyActivated)
        {
            //target.bodyType = RigidbodyType2D.Dynamic;
            // Activate the enemy script
            target.bodyType = RigidbodyType2D.Dynamic;
            _enemyActivated = true;
        }
    }
}
