using UnityEngine;

public class AdulthoodObjectBarrier : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (IsMonitoredTag(other.tag))
        {
            Rigidbody rb = other.attachedRigidbody;

            if (rb != null)
            {
                // Stop the object's movement
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // Optional pushback
                Vector3 pushDir = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDir * 5f, ForceMode.VelocityChange);
            }
        }
    }

    private bool IsMonitoredTag(string tag)
    {
        return tag == "LifeParcel" || tag == "WorkParcel" || tag == "KeyPhoto";
    }
}