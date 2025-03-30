using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleController : MonoBehaviour
{
    public GameObject subtitlePanel;
    public TextMeshProUGUI subtitleText;

    void Update()
    {
        subtitlePanel.SetActive(!string.IsNullOrEmpty(subtitleText.text));
    }
}
