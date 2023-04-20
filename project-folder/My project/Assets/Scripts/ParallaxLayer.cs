using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float startOffset;
    public float endOffset;

    private float _length;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float targetX = target.position.x;

        float distance = (targetX * speed);

        transform.position = new Vector3(_startPosition.x + distance, transform.position.y, transform.position.z);

        float minX = _startPosition.x + startOffset;
        float maxX = _startPosition.x + _length - endOffset;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }
}