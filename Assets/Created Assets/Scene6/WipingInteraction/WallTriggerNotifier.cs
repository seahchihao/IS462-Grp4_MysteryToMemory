using UnityEngine;

public class WallTriggerNotifier : MonoBehaviour
{
    public enum TriggerSide { Left, Right }
    public TriggerSide side;

    public WallWipeController controller;

    void OnTriggerEnter(Collider other)
    {
        controller.OnTriggerActivated(side, other.transform.position);
    }
}
