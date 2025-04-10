using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchInteraction : MonoBehaviour
{
    public GameObject hiddenPhotoPrefab; // Reference to the hidden photo prefab
    public int requiredFlowers = 2; // Number of flowers required to trigger the photo

    private int flowersOnBench = 0; // Counter for flowers placed on the bench

    private void Start()
    {
        hiddenPhotoPrefab.SetActive(false); // Ensure the photo is hidden initially
    }

    // Called when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object placed on the bench is a flower prefab
        if (other.CompareTag("Flower"))
        {
            flowersOnBench++;

            // If 2 flowers are placed on the bench, unveil the photo
            if (flowersOnBench >= requiredFlowers)
            {
                hiddenPhotoPrefab.SetActive(true);
            }
        }
    }

    // Called when another collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // If a flower is removed from the bench, decrease the counter
        if (other.CompareTag("Flower"))
        {
            flowersOnBench--;

            // If less than 2 flowers are left, keep the photo hidden
            if (flowersOnBench < requiredFlowers)
            {
                hiddenPhotoPrefab.SetActive(false);
            }
        }
    }
}
