using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public SceneTransitionManager sceneTransitionManager;
    public int sceneIndexToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneTransitionManager != null)
            {
                sceneTransitionManager.GoToSceneAsync(sceneIndexToLoad);
            }
            else
            {
                Debug.LogWarning("SceneTransitionManager is not assigned.");
            }
        }
    }
}