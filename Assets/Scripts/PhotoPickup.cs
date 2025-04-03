using UnityEngine;
using BNG;

public class PhotoPickup : MonoBehaviour
{
    public AudioObject clipToPlay;
    private bool audioPlayed = false;

    public void PlayNarration()
    {
        if (!audioPlayed)
        {
            Narration.instance.Say(clipToPlay);
            audioPlayed = true;
        }
    }
}
