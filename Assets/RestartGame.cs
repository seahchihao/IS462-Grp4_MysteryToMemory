using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartToSceneZero()
    {
        SceneManager.LoadScene(0);
    }
}
