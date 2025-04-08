using UnityEngine;
using BNG;

public class ItemPickup : MonoBehaviour
{
    public AudioObject clipToPlay;

    public void PlayNarration()
    {
        Narration.instance.Say(clipToPlay);
    }
}
