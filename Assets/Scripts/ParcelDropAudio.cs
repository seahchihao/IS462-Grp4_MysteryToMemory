using UnityEngine;

public class ParcelDropAudio : MonoBehaviour
{
    public AudioClip soundEffect; // The audio clip to play
    private AudioSource audioSource; // AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parcel"))
        {
            if (audioSource != null && soundEffect != null)
            {
                audioSource.PlayOneShot(soundEffect); // Play the sound when the "Box" tag hits
            }
        }
    }
}