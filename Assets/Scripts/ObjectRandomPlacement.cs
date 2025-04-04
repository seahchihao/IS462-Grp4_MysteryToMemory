using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomPlacement : MonoBehaviour
{
    public GameObject[] prefabs;  // Array to hold the prefabs
    public MeshCollider boundary;  // Reference to the boundary BoxCollider
    public int numberOfObjects = 10; // Number of prefabs to spawn
    public float minDistance = 0.5f; // Minimum distance between objects to avoid overlap
    public float minY = 0.5f;
    public float maxY = 0.5f;
    public bool enableRotation = true;

    private List<Vector3> spawnPositions = new List<Vector3>();  // List to track spawn positions
    private List<GameObject> spawnedObjects = new List<GameObject>();  // List to track spawned objects

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Vector3 boundaryMin = boundary.bounds.min;
        Vector3 boundaryMax = boundary.bounds.max;

        int uniquePrefabsUsed = 0; // Counter to track how many unique prefabs we've used

        // Loop to spawn the desired number of objects
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Select a random prefab from the array
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

            // Try to find a valid spawn position
            Vector3 randomPosition = FindValidPosition(boundaryMin, boundaryMax);

            // Instantiate the prefab at the random position
            GameObject newObject = Instantiate(prefab, randomPosition, Quaternion.identity);

            // Add the position to the list to track it
            spawnPositions.Add(randomPosition);

            // Add the RotationController script if rotation is enabled
            if (enableRotation)
            {
                RotationController rotationController = newObject.AddComponent<RotationController>();
            }

            // Check if the prefab is unique, if so, increment the counter
            if (!spawnedObjects.Contains(newObject))
            {
                uniquePrefabsUsed++;
            }

            // If all unique items have been spawned, allow duplicates
            if (uniquePrefabsUsed == prefabs.Length)
            {
                break; // Stop trying to find unique prefabs
            }

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
                Random.Range(minY, maxY),
                Random.Range(boundaryMin.z, boundaryMax.z)  // z within the boundary
            );

            // Check if the new position is too close to any existing item
            isValidPosition = true;
            foreach (Vector3 pos in spawnPositions)
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

public class RotationController : MonoBehaviour
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