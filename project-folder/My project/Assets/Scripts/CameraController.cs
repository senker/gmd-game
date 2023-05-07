/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 playerPos = player.position;
        Vector3 cameraPos = transform.position;

        if (playerPos.y > 1f)
        {
            cameraPos = new Vector3(playerPos.x, playerPos.y + 0.2f, cameraPos.z);
        }
        else
        {
            cameraPos = new Vector3(playerPos.x, playerPos.y + 0.9f, cameraPos.z);
        }

       // transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, smoothTime);
    }
}*/
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

        if (playerPos.y > 1f)
        {
            cameraTransform.position = new Vector3(playerPos.x, playerPos.y + 0.1f, cameraTransform.position.z);
        }
        else
        {
            cameraTransform.position = new Vector3(playerPos.x, playerPos.y + 0.9f, cameraTransform.position.z);

        }
    }
}
