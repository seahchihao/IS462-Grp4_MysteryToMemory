using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetGO;

    private bool triggerUp = true;

    private void OnTriggerDown() {
        if (triggerUp) {
            targetGO.SetActive(!targetGO.activeSelf);
            triggerUp = false;
        }
    }

    private void OnTriggerUp() {
        triggerUp = true;
    }

    public void OnTrigger(float value) {
        if (value == 0) OnTriggerUp();
        else if (value == 1) OnTriggerDown();
    }
}
