using UnityEngine;

public class RevealOnPlayerTouch : MonoBehaviour
{
    [SerializeField] private GameObject objectToReveal; // ¡û This line makes it visible in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            if (objectToReveal != null)
            {
                objectToReveal.SetActive(true);
            }
        }
    }
}
