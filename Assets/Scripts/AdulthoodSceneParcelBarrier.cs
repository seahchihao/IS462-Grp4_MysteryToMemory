using UnityEngine;

public class AdulthoodSceneParcelBarrier : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Parcel"))
        {
            Rigidbody rb = other.attachedRigidbody;

            if (rb != null)
            {
                // Stop the Parcel's movement
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // Optional pushback
                Vector3 pushDir = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDir * 5f, ForceMode.VelocityChange);
            }
        }
    }
}