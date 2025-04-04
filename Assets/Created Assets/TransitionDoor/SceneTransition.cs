using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("Drag scene asset here")]
    public Object nextScene; // Drag scene from Project window

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get scene name from asset
            string sceneName = nextScene != null ? nextScene.name : "";
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("No scene assigned!");
            }
        }
    }
}
