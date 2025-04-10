using UnityEngine;

public class PickUpFlower : MonoBehaviour
{
    private Camera mainCamera;
    private bool isPickedUp = false;
    private Transform flowerTransform;
    private Rigidbody flowerRigidbody;
    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main; // Get the main camera
        flowerTransform = transform; // Get the transform of the flower
        flowerRigidbody = flowerTransform.GetComponent<Rigidbody>(); // Get the flower's Rigidbody
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to pick up the flower
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == flowerTransform)
                {
                    isPickedUp = true;
                    flowerRigidbody.isKinematic = true; // Temporarily disable physics on pick-up
                    offset = flowerTransform.position - hit.point; // Calculate the offset between mouse and flower position
                }
            }
        }

        if (isPickedUp)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                flowerTransform.position = hit.point + offset; // Move flower to mouse position
            }
        }

        if (Input.GetMouseButtonUp(0)) // Release the flower when mouse button is released
        {
            isPickedUp = false;
            flowerRigidbody.isKinematic = false; // Restore physics interaction after release
        }
    }
}
