using UnityEngine;

public class FloatingOrb : MonoBehaviour
{
    [Header("Floating Settings")]
    public float speed = 1f; // Speed of the floating motion
    public float amplitude = 1f; // Amplitude of the floating motion
    public float minHeight = 1f; // Minimum height for the orb
    public float maxHeight = 5f; // Maximum height for the orb

    private float initialY; // The starting Y position of the orb

    void Start()
    {
        // Record the initial Y position of the orb
        initialY = transform.position.y;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = initialY + Mathf.Sin(Time.time * speed) * amplitude;

        // Clamp the Y position to stay within minHeight and maxHeight
        newY = Mathf.Clamp(newY, minHeight, maxHeight);

        // Update the orb's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
