using System.Collections;
using TMPro;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;
    public static Subtitle instance;

    private void Awake()
    {
        instance = this;
        ClearSubtitle();
    }

    public void SetSubtitle(string subtitle, float delay)
    {
        subtitleText.text = subtitle;
        StartCoroutine(ClearAfterSeconds(delay));
    }

    public void ClearSubtitle()
    {
        subtitleText.text = "";
    }

    private IEnumerator ClearAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearSubtitle();
    }
}
