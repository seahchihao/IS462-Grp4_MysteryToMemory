using BNG;  // Assuming BNG is handling input
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public InputBridge inputBridge;

    // Update is called once per frame
    void Update()
    {
        // Check if the A button is pressed
        if (inputBridge.AButtonDown && inputBridge.XButtonDown)
        {
            ResetGame();
        }
    }

    // Method to reset the game (reload the current scene)
    void ResetGame()
    {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Reload the current scene to reset the game
        SceneManager.LoadScene(currentSceneName);
    }
}
