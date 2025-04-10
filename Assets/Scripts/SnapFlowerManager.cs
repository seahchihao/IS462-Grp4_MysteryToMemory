using UnityEngine;

public class SnapFlowerManager : MonoBehaviour
{
    public static int num_snaps = 0;

    public GameObject old_age_photo_GO;
    private static GameObject old_age_photo;

    private void Start()
    {
        old_age_photo = old_age_photo_GO;
    }

    public static void increment_count()
    {
        num_snaps++;

        Debug.Log("[SnapFlowerManager] num_snaps: " + num_snaps);

        // If num_snaps is equal to 2
        // Make old_age_photo isActive(TRUE)

        if (num_snaps == 2)
        {
            old_age_photo.SetActive(true);
        }
    }

}
