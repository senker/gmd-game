using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private void Update()
    {
        var playerPos = player.position;
        var cameraTransform = transform;
        cameraTransform.position = new Vector3(playerPos.x, playerPos.y + 0.9f, cameraTransform.position.z);
    }
}
