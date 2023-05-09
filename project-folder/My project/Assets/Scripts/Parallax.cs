using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float _length, _startPos;
    public GameObject cam;
    public float parallaxEffect;
    
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        var position = cam.transform.position;
        float dist = (position.x * parallaxEffect);

        var transform1 = transform;
        var position1 = transform1.position;
        position1 = new Vector3(_startPos + dist, position1.y, position1.z);
        transform1.position = position1;
        
        float temp = (position.x * (1 - parallaxEffect));

        if (temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos - _length) _startPos -= _length;
    }
}
