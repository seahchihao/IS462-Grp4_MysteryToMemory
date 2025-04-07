using UnityEngine;

public class PlayAudioOnGrab : MonoBehaviour
{
    public AudioClip grabSound;  // The sound to play (assign in Inspector)
    private AudioSource audioSource;  // AudioSource component

    void Start()
    {
        // Ensure AudioSource component is attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // Adds AudioSource if not already present
        }

        // Log if the AudioSource is attached properly
        if (audioSource != null)
        {
            Debug.Log("AudioSource attached successfully!");
        }
        else
        {
            Debug.LogWarning("AudioSource is missing on this object!");
        }
    }

    public void PlayGrabSound()
    {
        // Check if the audio clip is assigned
        if (grabSound != null)
        {
            if (audioSource != null)
            {
                Debug.Log("Playing grab sound...");  // Log when the sound is being played
                audioSource.PlayOneShot(grabSound);  // Play the sound using PlayOneShot
            }
            else
            {
                Debug.LogError("AudioSource is missing! Cannot play sound.");
            }
        }
        else
        {
            Debug.LogWarning("Grab sound is missing! Please assign an AudioClip.");
        }
    }
}
