using UnityEngine;

public class RevealWhenFlowersPlaced : MonoBehaviour
{
    [Header("Flower Prefabs to Detect")]
    public GameObject flower1;
    public GameObject flower2;

    [Header("Photo to Reveal")]
    public GameObject hiddenPhoto;

    private bool flower1OnBench = false;
    private bool flower2OnBench = false;

    void Start()
    {
        if (hiddenPhoto != null)
            hiddenPhoto.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == flower1)
            flower1OnBench = true;

        if (other.gameObject == flower2)
            flower2OnBench = true;

        TryRevealPhoto();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == flower1)
            flower1OnBench = false;

        if (other.gameObject == flower2)
            flower2OnBench = false;
    }

    void TryRevealPhoto()
    {
        if (flower1OnBench && flower2OnBench)
        {
            if (hiddenPhoto != null)
                hiddenPhoto.SetActive(true);
        }
    }
}
