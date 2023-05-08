using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Detection : MonoBehaviour
{
    public Rigidbody2D target;
    private bool _enemyActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && !_enemyActivated)
        {
            target.bodyType = RigidbodyType2D.Dynamic;
            _enemyActivated = true;
        }
    }
}
