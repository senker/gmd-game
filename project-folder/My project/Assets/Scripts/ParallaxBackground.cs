using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform playerTransform;
    public float movementSpeed = 0.1f;
    public float slowDownFactor = 0.5f;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = playerTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = playerTransform.position - lastPlayerPosition;
        transform.position += new Vector3(deltaMovement.x * movementSpeed, deltaMovement.y * movementSpeed, 0f);

        if (deltaMovement.magnitude > 0.001f)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, movementSpeed * slowDownFactor, Time.deltaTime);
        }
        else
        {
            movementSpeed = 0f;
        }

        lastPlayerPosition = playerTransform.position;
    }
}