using UnityEngine;
 // Add this if using XR Toolkit

public class AdulthoodKeyPhotoReveal : MonoBehaviour
{
    private bool revealed = false;
    private Renderer[] renderers;
    private Collider[] colliders;
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable; // NEW

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        colliders = GetComponentsInChildren<Collider>(true);
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>(); // NEW

        SetVisibility(false);
        SetInteraction(false);
    }

    void Update()
    {
        if (!revealed && AllParcelsDelivered())
        {
            RevealObject();
            revealed = true;
        }
    }

    private bool AllParcelsDelivered()
    {
        GameObject[] lifeParcels = GameObject.FindGameObjectsWithTag("LifeParcel");
        GameObject[] workParcels = GameObject.FindGameObjectsWithTag("WorkParcel");

        foreach (GameObject parcel in lifeParcels)
        {
            if (parcel.activeInHierarchy)
                return false;
        }

        foreach (GameObject parcel in workParcels)
        {
            if (parcel.activeInHierarchy)
                return false;
        }

        return true;
    }

    private void RevealObject()
    {
        SetVisibility(true);
        SetInteraction(true);
        Debug.Log("📸 KeyPhoto revealed and is now grabbable!");
    }

    private void SetVisibility(bool state)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = state;
        }
    }

    private void SetInteraction(bool state)
    {
        foreach (Collider c in colliders)
        {
            c.enabled = state;
        }

        if (rb != null)
            rb.isKinematic = !state;

        if (grabInteractable != null)
            grabInteractable.enabled = state; // ✨ Enable grabbing
    }
}