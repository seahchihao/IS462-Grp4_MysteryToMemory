using UnityEngine;

[CreateAssetMenu(fileName = "AudioObject", menuName = "Scriptable Objects/AudioObject")]
public class AudioObject : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;
}
