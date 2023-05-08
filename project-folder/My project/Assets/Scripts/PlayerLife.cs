using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private PlayerMovement _pm;
    public PlayerCombat target;
    [SerializeField] private AudioSource deathSoundEffect;

    private static readonly int Death = Animator.StringToHash("Death");

    void Start()
    {
        _pm = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void Die()
    {
        _pm.enabled = false;
        _rb.bodyType = RigidbodyType2D.Static;
        _anim.SetTrigger(Death);
        deathSoundEffect.Play();
        target.enabled = false;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
