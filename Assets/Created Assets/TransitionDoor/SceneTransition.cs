using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("Drag scene asset here")]
    public Object nextScene; // Drag scene from Project window

    [Tooltip("Required stay duration (seconds)")]
    public float transitionDelay = 1f;

    private bool isTransitioning = false;
    private bool playerInTrigger = false;
    private AsyncOperation asyncLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            playerInTrigger = true;
            StartCoroutine(TransitionProcess());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private IEnumerator TransitionProcess()
    {
        isTransitioning = true;

        // Get scene name from asset
        string sceneName = nextScene != null ? nextScene.name : "";
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("No scene assigned!");
            yield break;
        }

        // Start async loading
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        float timer = 0;
        bool loadingComplete = false;

        // Phase 1: Loading and waiting
        while (timer < transitionDelay || !loadingComplete)
        {
            if (!playerInTrigger)
            {
                CancelTransition();
                yield break;
            }

            // Update loading status
            loadingComplete = asyncLoad.progress >= 0.9f;

            // Update timer only while player remains
            timer += Time.deltaTime;
            yield return null;
        }

        // Phase 2: Final activation
        asyncLoad.allowSceneActivation = true;
        isTransitioning = false;
    }

    private void CancelTransition()
    {
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = true; // Force cleanup
            asyncLoad = null;
        }
        isTransitioning = false;
    }
}
