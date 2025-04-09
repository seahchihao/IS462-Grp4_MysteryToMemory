using UnityEngine;

public class ResetOnFall : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Save original spawn position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the item touches the floor, reset it
        if (other.CompareTag("Floor"))
        {
            ResetItem();
        }
    }

    void ResetItem()
    {
        // Reset transform
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Reset physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
