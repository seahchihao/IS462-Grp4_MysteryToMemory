using UnityEngine;

public class RandomPrefabPlacement : MonoBehaviour
{
    public GameObject[] prefabs;  // Array to hold the prefabs
    public Vector3 boundaryMin;   // Minimum point of the boundary
    public Vector3 boundaryMax;   // Maximum point of the boundary
    public int numberOfObjects = 10; // Number of prefabs to spawn

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Loop to spawn the desired number of objects
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Select a random prefab from the array
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

            // Generate a random position within the defined boundary
            Vector3 randomPosition = new Vector3(
                Random.Range(boundaryMin.x, boundaryMax.x),
                Random.Range(boundaryMin.y, boundaryMax.y),
                Random.Range(boundaryMin.z, boundaryMax.z)
            );

            // Instantiate the prefab at the random position
            Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }
}
