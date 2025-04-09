using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("Enter the name of the next scene to load")]
    public string nextSceneName; // Scene name to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Debug.Log($"Loading scene: {nextSceneName}");
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("No scene name assigned! Please set the 'Next Scene Name' field in the Inspector.");
            }
        }
    }
}
