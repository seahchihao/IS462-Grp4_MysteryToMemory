using UnityEngine;
using BNG;

public class ButtonTrigger : MonoBehaviour
{
    public AudioObject clipToPlay;
    private bool audioPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object touching us is a VR hand
        if (!audioPlayed && other.GetComponentInParent<HandController>() != null)
        {
            Narration.instance.Say(clipToPlay);
            audioPlayed = true;
        }
    }
}
