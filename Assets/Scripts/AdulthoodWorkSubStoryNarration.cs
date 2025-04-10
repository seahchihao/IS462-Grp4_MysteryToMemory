using UnityEngine;

public class AdulthoodWorkSubStoryNarration : MonoBehaviour
{
    public AudioObject clipToPlay;
    private bool audioPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WorkParcel") && !audioPlayed)
        {
            if (Narration.instance != null)
            {
                Narration.instance.Say(clipToPlay);
                audioPlayed = true;
            }
            else
            {
                Debug.LogError("Narration instance is null. Make sure you have a GameObject with the Narration script in your scene.");
            }
        }
    }
}