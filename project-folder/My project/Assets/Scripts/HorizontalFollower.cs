using UnityEngine;

public class HorizontalFollower : MonoBehaviour
{
    private Transform _cameraTransform;
    private float _originalYPosition;
    public Transform characterTransform;

    private void Start()
    {
        if (Camera.main != null) _cameraTransform = Camera.main.transform;
        _originalYPosition = transform.position.y;
    }

    private void Update()
    {

        float cameraXPosition = _cameraTransform.position.x;
        float objectYPosition = _originalYPosition;
        var transform1 = transform;
        float objectXPosition = transform1.position.x;
        transform.position = new Vector3(characterTransform.position.x, transform.position.y, transform.position.z);

        transform1.position = new Vector3(cameraXPosition, objectYPosition, objectXPosition);
    }
}