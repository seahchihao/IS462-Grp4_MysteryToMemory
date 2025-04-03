using BNG;
using UnityEngine;
using UnityEngine.XR;

public class UponGrabbingKey : MonoBehaviour
{

    public string keyTag = "KeyPhoto"; // Tag to identify the key object
    public GameObject itemObject;  // The object that already exists in the scene
    public GameObject uiElement;   // The UI element to toggle visibility
    public float spawnDistance = 2f;// The distance in front/behind the player
    private InputBridge inputBridge;
    private bool hasPickedUpObject = false; // Flag to check if object is picked up
    private bool isUiVisible = false; // UI visibility toggle state
    private Transform playerTransform; // Player's transform

    void Start()
    {
        // Initially hide the UI element
        uiElement.SetActive(false);

        // Initially hide the item (it is placed somewhere in the scene, but hidden)
        itemObject.SetActive(false); // Make sure it starts off as invisible

        // Find the player's transform (you may already have a reference to it in your scene)
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the Y button is pressed on the Oculus controller
        if (inputBridge.YButtonDown)
        {
            if (hasPickedUpObject)
            { 
                ToggleUI();
            }
        }
    }

    private void ToggleUI()
    {
        // Toggle the UI visibility
        isUiVisible = !isUiVisible;
        uiElement.SetActive(isUiVisible);
    }

    // Detect if player picks up the object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(keyTag)) // Check if the player has collided with the Key
        {
            // Pick up the object and spawn the in-game item and UI element
            hasPickedUpObject = true;
            uiElement.SetActive(isUiVisible);
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if (playerTransform != null)
        {
            // Calculate the spawn position relative to the player's forward direction
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;

            // Move the item to the new position and make it visible
            itemObject.transform.position = spawnPosition;
            itemObject.SetActive(true); // Make the object visible when picked up
        }
    }
}

