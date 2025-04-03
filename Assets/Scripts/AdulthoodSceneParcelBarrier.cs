using UnityEngine;

public class AdulthoodSceneParcelBarrier : MonoBehaviour
{
    // This function is called when another collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided has the "MainParcel" tag
        if (other.CompareTag("MainParcel"))
        {
            // If the tag is "MainParcel", activate the collider
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
        // Check if the object that collided has the "Parcel" tag
        else if (other.CompareTag("Parcel"))
        {
            // If the tag is "Parcel", allow the object to pass through
            // Do nothing, this is just a placeholder to show intent
            // (The default behavior of trigger colliders allows this)
        }
    }

    // Optional: If you want to ensure the collider is disabled when the object exits the trigger
    private void OnTriggerExit(Collider other)
    {
        // Optionally deactivate collider or reset the state when the object exits
        if (other.CompareTag("MainParcel"))
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}