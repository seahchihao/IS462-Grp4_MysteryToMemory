using System.Collections;
using UnityEngine;
using TMPro;

public class Subtitle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;
    public static Subtitle instance;

    private Coroutine subtitleCoroutine;

    private void Awake()
    {
        instance = this;
        ClearSubtitle();
    }

    public void SetSubtitle(string subtitle, float totalDuration)
    {
        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);

        subtitleCoroutine = StartCoroutine(TypeSubtitleCharByChar(subtitle, totalDuration));
    }

    public void ClearSubtitle()
    {
        if (subtitleCoroutine != null)
            StopCoroutine(subtitleCoroutine);

        subtitleText.text = "";
    }

    private IEnumerator TypeSubtitleCharByChar(string subtitle, float totalDuration)
    {
        subtitleText.text = "";

        float delayPerChar = totalDuration / subtitle.Length;

        for (int i = 0; i < subtitle.Length; i++)
        {
            subtitleText.text += subtitle[i];
            yield return new WaitForSeconds(delayPerChar);
        }

        // Optional: wait a moment before clearing
        yield return new WaitForSeconds(1f);
        ClearSubtitle();
    }
}
