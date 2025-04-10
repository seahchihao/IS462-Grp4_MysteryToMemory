using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startScreen;
    public CharacterController playerController;
    
    void Start()
    {
        startScreen.SetActive(true);
        playerController.enabled = false;
    }

    public void GameStart()
    {
        startScreen.SetActive(false);
        playerController.enabled = true;
    }
}
