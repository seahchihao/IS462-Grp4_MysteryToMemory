using Unity.AI.Navigation;
using UnityEngine;

public class Door : MonoBehaviour
{
    private NavMeshLink navMeshLink;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshLink = GetComponent<NavMeshLink>();
    }

    public void OpenDoor()
    {
        navMeshLink.enabled = true;
    }
    public void CloseDoor()
    {
        navMeshLink.enabled = false;
    }
}
