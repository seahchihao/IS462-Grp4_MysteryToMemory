using UnityEngine;

public class GrabNotebookManager : MonoBehaviour
{
    public GameObject paperPlane_GO;

    private void Start()
    {
        paperPlane_GO.SetActive(false);
    }

    public void OnNotebookGrabbed()  // Now non-static
    {
        Debug.Log("[GrabNotebookManager] Notebook grabbed!");
        paperPlane_GO.SetActive(true);
    }
}
