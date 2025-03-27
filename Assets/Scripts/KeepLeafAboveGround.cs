using UnityEngine;

public class KeepLeafAboveGround : MonoBehaviour
{
    public float heightAboveGround = 0.5f; // The height above the ground where the leaf should stay.
    public LayerMask groundLayer; // The layer of the ground to cast the ray against.

    private void Update()
    {
        // Cast a ray downward from the leaf's position to check for the ground.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Adjust the leaf's position to stay above the ground.
            float targetHeight = hit.point.y + heightAboveGround;
            Vector3 targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);

            // Set the leaf's position to the target position.
            transform.position = targetPosition;
        }
    }
}
