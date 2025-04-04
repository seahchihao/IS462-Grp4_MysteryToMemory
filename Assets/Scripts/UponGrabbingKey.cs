using BNG;
using UnityEngine;
using UnityEngine.XR;

public class UponGrabbingKey : MonoBehaviour
{
    public GameObject itemObject;  // The object that already exists in the scene
    public GameObject uiElement;   // The UI element to toggle visibility
    private InputBridge inputBridge;
    private bool hasPickedUpObject = false; // Flag to check if object is picked up
    private bool isUiVisible = false; // UI visibility toggle state
    private Transform playerTransform; // Player's transform

    // The specific spot near the door where you want to move the player
    public float distanceInFrontOfDoor = 8f; // Distance to move the player in front of the door
    private Transform doorTransform;

    public void OnKeyPickedUp()
    {
        hasPickedUpObject = true;
        isUiVisible = true;

        // Assign doorTransform to itemObject's transform
        doorTransform = itemObject.transform;

        // Find the player's transform (you may already have a reference to it in your scene)
        playerTransform = GameObject.FindWithTag("Player").transform;

        // Pick up the object and spawn the in-game item and UI element
        uiElement.SetActive(isUiVisible);
        SpawnItem();

        // Move the player to the target position near the door and make them face it
        MovePlayerToSpotInFrontOfDoor();
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

    private void SpawnItem()
    {
        itemObject.SetActive(true); // Make the object visible when picked up
    }
    private void MovePlayerToSpotInFrontOfDoor()
    {
        if (doorTransform != null)
        {
            // Calculate the position in front of the door by moving the player a fixed distance
            Vector3 positionInFrontOfDoor = doorTransform.position + doorTransform.forward * distanceInFrontOfDoor;

            // Move the player to the calculated position
            playerTransform.position = positionInFrontOfDoor;

            // Make the player face the door by rotating the player to look at the door
            playerTransform.rotation = Quaternion.LookRotation(doorTransform.position - playerTransform.position);
        }
    }
}

