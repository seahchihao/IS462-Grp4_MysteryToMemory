using BNG;
using UnityEngine;

public class SceneLoaderOnTrigger : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public string sceneToLoad;

    void Start()
    {
        if (sceneLoader == null)
        {
            Debug.LogError("SceneLoader script is not assigned.");
        }

        if (sceneToLoad == null)
        {
            Debug.LogError("SceneToLoad is not set.");
        }
    }

    void OnTriggerEnter()
    {
        sceneLoader.LoadScene(sceneToLoad);
    }
}