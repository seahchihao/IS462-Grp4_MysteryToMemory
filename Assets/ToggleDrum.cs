using UnityEngine;

public class ToggleDrum : MonoBehaviour
{
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleAudio() {
        if (audioSource == null) return;

        if (audioSource.isPlaying)
            audioSource.Stop();
        else
            audioSource.Play();
    }
}
