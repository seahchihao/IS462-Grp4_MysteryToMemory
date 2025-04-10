using UnityEngine;

public class BenchTrigger : MonoBehaviour
{
    public GameObject hiddenPrefab; // The hidden prefab that appears when flowers are placed
    private int flowersOnBench = 0; // Counter to track how many flowers are on the bench

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is a flower
        if (other.CompareTag("Flower"))
        {
            flowersOnBench++; // Increment the count of flowers on the bench

            // Optionally, you can disable the flower’s collider to prevent it from triggering multiple times
            other.GetComponent<Collider>().enabled = false;

            // If both flowers are on the bench, trigger the hidden prefab
            if (flowersOnBench == 2)
            {
                hiddenPrefab.SetActive(true); // Make the hidden prefab appear
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that exited is a flower
        if (other.CompareTag("Flower"))
        {
            flowersOnBench--; // Decrement the count of flowers on the bench

            // If you want the hidden prefab to disappear when a flower is removed, uncomment this line
            // if (flowersOnBench < 2)
            // {
            //     hiddenPrefab.SetActive(false);
            // }
        }
    }
}
