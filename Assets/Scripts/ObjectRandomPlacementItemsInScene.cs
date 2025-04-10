using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomPlacementItemsInScene : MonoBehaviour
{
    public GameObject[] objectsInScene;  // Array to hold objects already in the scene
    public MeshCollider boundary;  // Reference to the boundary BoxCollider
    public float minDistance = 0.5f; // Minimum distance between objects to avoid overlap
    public float minY = 0.5f;  // Minimum Y (height) for the object placement
    public float maxY = 0.5f;  // Maximum Y (height) for the object placement
    public bool enableRotation = true;  // Flag to enable/disable rotation

    private List<Vector3> placedPositions = new List<Vector3>();  // List to track positions of placed objects

    void Start()
    {
        RandomlyRearrangeObjects();
    }

    // Method to randomly rearrange objects within the defined boundary
    void RandomlyRearrangeObjects()
    {
        Vector3 boundaryMin = boundary.bounds.min;
        Vector3 boundaryMax = boundary.bounds.max;

        // Loop through all objects in the scene
        foreach (GameObject obj in objectsInScene)
        {
            // Try to find a valid position for each object
            Vector3 randomPosition = FindValidPosition(boundaryMin, boundaryMax);

            // Move the object to the new random position
            obj.transform.position = randomPosition;

            // Optionally, add rotation control if enabled
            if (enableRotation)
            {
                // Only add the RotationController if it doesn't already exist on the object
                if (obj.GetComponent<RotationController>() == null)
                {
                    RotationController rotationController = obj.AddComponent<RotationController>();
                }
            }

            // Track the position to ensure no overlap
            placedPositions.Add(randomPosition);
        }
    }

    // Find a valid position that is not within the minimum distance of any other item
    Vector3 FindValidPosition(Vector3 boundaryMin, Vector3 boundaryMax)
    {
        Vector3 randomPosition = Vector3.zero;
        bool isValidPosition = false;

        // Keep trying until we find a valid position
        while (!isValidPosition)
        {
            randomPosition = new Vector3(
                Random.Range(boundaryMin.x, boundaryMax.x), // x within the boundary
                Random.Range(minY, maxY),  // Random Y (height) within the boundary
                Random.Range(boundaryMin.z, boundaryMax.z)  // z within the boundary
            );

            // Check if the new position is too close to any existing item
            isValidPosition = true;
            foreach (Vector3 pos in placedPositions)
            {
                if (Vector3.Distance(randomPosition, pos) < minDistance)
                {
                    isValidPosition = false;
                    break; // Exit loop early if the position is too close to another
                }
            }
        }

        return randomPosition;
    }
}

public class RotationControllerItemsInScene : MonoBehaviour
{
    private float rotationSpeedX; // Rotation speed around the X-axis
    private float rotationSpeedY; // Rotation speed around the Y-axis
    private float rotationSpeedZ; // Rotation speed around the Z-axis

    void Start()
    {
        // Initialize random rotation speeds for each axis
        rotationSpeedX = Random.Range(10f, 50f); // Random rotation speed between 10 and 50 for X-axis
        rotationSpeedY = Random.Range(10f, 50f); // Random rotation speed between 10 and 50 for Y-axis
        rotationSpeedZ = Random.Range(10f, 50f); // Random rotation speed between 10 and 50 for Z-axis
    }

    void Update()
    {
        // Rotate the object around its X, Y, and Z axes with the corresponding random speeds
        transform.Rotate(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);
    }
}
