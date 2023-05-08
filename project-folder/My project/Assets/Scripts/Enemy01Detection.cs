using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Detection : MonoBehaviour
{
    public Enemy enemyScript;
    private bool _enemyActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && !_enemyActivated)
        {
            enemyScript.enabled = true;
            _enemyActivated = true;
        }
    }
}
