using UnityEngine;

public class Narration : MonoBehaviour
{
    private AudioSource source;
    public static Narration instance;

    private void Awake()
    {
      if (instance == null)
      {
        instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else if (instance != this)
      {
        Destroy(gameObject);
      }
    }

    private void Start()
    {
      source = gameObject.AddComponent<AudioSource>();
    }

    public void Say(AudioClip clip)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.PlayOneShot(clip);
    }
}
