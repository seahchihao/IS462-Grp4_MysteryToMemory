using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float followDistance = 2f;
    public float heightOffset = -0.5f;
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 targetPos = cameraTransform.position + cameraTransform.forward * followDistance;
        targetPos.y += heightOffset;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180f, 0); // Flip to face the user
    }
}
