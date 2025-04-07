using UnityEngine;

public class AdulthoodWorkParcelChecker : MonoBehaviour
{
    public AudioClip deliverySound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // Auto-add AudioSource if missing
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WorkParcel"))
        {
            // Deactivate the parcel
            other.gameObject.SetActive(false);

            // Play the delivery sound at the delivery zone
            if (deliverySound != null)
            {
                audioSource.PlayOneShot(deliverySound);
            }

            Debug.Log("Parcel delivered!");
        }
    }
}