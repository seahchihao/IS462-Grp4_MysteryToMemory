using UnityEngine;

public class OnGrab : MonoBehaviour
{
    private Rigidbody leafRigidbody; // Reference to the leaf's Rigidbody

    void Start()
    {
        // Ensure you have a Rigidbody attached to the leaf
        leafRigidbody = GetComponent<Rigidbody>();
    }

    // This method will be called when the leaf is grabbed
    public void OnGrabObject()
    {
        leafRigidbody.isKinematic = true; // Prevent physics from affecting the leaf while being held
    }

    // This method will be called when the leaf is dropped
    public void OnDropObject()
    {
        leafRigidbody.isKinematic = false; // Re-enable physics when the leaf is dropped
    }
}
