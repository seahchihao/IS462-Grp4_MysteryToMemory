using UnityEngine;

public class ResetPrefabPosition : MonoBehaviour
{
    private Vector3 originalPosition; // Store the original position
    private bool isDropped = false; // Flag to check if the object has dropped
    public string floorTag = "Floor"; // The floor tag to detect collision with the floor

    void Start()
    {
        // Store the original position when the script starts
        originalPosition = transform.position;
    }

    void Update()
    {
        // Reset the position if the object is touching the floor
        if (!isDropped && IsOnFloor())
        {
            ResetPosition();
        }
    }

    // Check if the object is on the floor by checking if it collides with an object with the "Floor" tag
    bool IsOnFloor()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // A small radius to check around the prefab
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(floorTag)) // Check if the collided object has the "Floor" tag
            {
                return true;
            }
        }
        return false;
    }

    // Reset the position to the original position
    void ResetPosition()
    {
        transform.position = originalPosition;
        isDropped = true; // Mark the object as dropped
    }
}
