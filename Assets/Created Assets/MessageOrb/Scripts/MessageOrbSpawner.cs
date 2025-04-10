using UnityEngine;
using TMPro;

public class MessageOrbSpawner : MonoBehaviour
{
    [Header("Orb Settings")]
    public GameObject orbPrefab; // Prefab for the glowing orb
    public Transform spawnArea; // Plane or area where orbs will spawn
    public TMP_InputField inputField; // VR keyboard input field

    [Header("Floating Height Settings")]
    public float minHeight = 1f; // Minimum height for floating orbs
    public float maxHeight = 5f; // Maximum height for floating orbs

    public void SpawnOrb()
    {
        // Ensure the input field is not empty
        if (!string.IsNullOrEmpty(inputField.text))
        {
            // Create a new orb at a random position
            GameObject newOrb = Instantiate(orbPrefab, GetRandomPosition(), Quaternion.identity);

            // Set the message text inside the orb
            TMP_Text orbText = newOrb.GetComponentInChildren<TMP_Text>();
            if (orbText != null)
            {
                orbText.text = inputField.text;
            }

            // Clear the input field after submission
            inputField.text = "";
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Get the bounds of the spawn area (plane)
        Renderer planeRenderer = spawnArea.GetComponent<Renderer>();
        Vector3 planeSize = planeRenderer.bounds.size;
        Vector3 planeCenter = planeRenderer.bounds.center;

        // Generate random X and Z positions within the bounds of the plane
        float randomX = Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float randomZ = Random.Range(-planeSize.z / 2, planeSize.z / 2);

        // Generate a random Y position for floating height
        float randomY = Random.Range(minHeight, maxHeight);

        // Return the calculated position
        return new Vector3(
            planeCenter.x + randomX,
            planeCenter.y + randomY,
            planeCenter.z + randomZ
        );
    }
}
