using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float _horizontal;
    private float _speed = 8f;
    private float _jumpingPower = 16f;
    private bool _isFacingRight = true;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
