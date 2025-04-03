using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform cameraTransform;

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Rotate the subtitle canvas to always face the camera
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180f, 0); // Flip around if text is backward
    }
}

