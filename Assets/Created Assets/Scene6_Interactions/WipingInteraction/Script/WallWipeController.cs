using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Renderer))]
public class WallWipeController : MonoBehaviour
{
    [Header("Visual Settings")]
    [ColorUsage(true, true)] public Color initialColor = Color.gray;
    [ColorUsage(true, true)] public Color targetColor = Color.white;
    public float brightenAmount = 0.2f;
    public float maxBrightness = 1f;

    [Header("VFX Settings")]
    public GameObject vfxPrefab;
    public Vector3 vfxScale = Vector3.one * 0.5f;
    public float vfxLifetime = 2f;

    [Header("Fade Settings")]
    public float inactivityDelay = 2f;         // Time after last trigger before fade starts
    public float fadeSpeed = 0.2f;             // Brightness units per second during fade
    private bool isFading = false;

    [Header("Max Brightness VFX")]
    public GameObject vfxMaxBrightnessPrefab;
    public Vector3 vfxMaxBrightnessScale = Vector3.one;
    public float vfxMaxBrightnessLifetime = 3f;
    private bool hasReachedMax = false;

    private float lastTriggerTimestamp = -999f;

    private Renderer wallRenderer;
    private MaterialPropertyBlock mpb;
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    private float currentBrightness;

    private enum LastTrigger { None, Left, Right }
    private LastTrigger lastTrigger = LastTrigger.None;

    void Awake()
    {
        wallRenderer = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        InitializeMaterial();
    }

    void Update()
    {
        if (hasReachedMax || currentBrightness <= 0f) return;

        if (!isFading && Time.time - lastTriggerTimestamp > inactivityDelay)
        {
            isFading = true;
        }

        if (isFading)
        {
            currentBrightness -= fadeSpeed * Time.deltaTime;
            currentBrightness = Mathf.Max(0f, currentBrightness);
            UpdateMaterialBrightness();
        }
    }


    void InitializeMaterial()
    {
        wallRenderer.GetPropertyBlock(mpb);
        mpb.SetColor(BaseColor, initialColor);
        wallRenderer.SetPropertyBlock(mpb);
        currentBrightness = 0f;
        lastTrigger = LastTrigger.None;
    }

    public void OnTriggerActivated(WallTriggerNotifier.TriggerSide side, Vector3 vfxPosition)
    {
        if (currentBrightness >= maxBrightness) return;

        if ((side == WallTriggerNotifier.TriggerSide.Left && lastTrigger != LastTrigger.Left) ||
            (side == WallTriggerNotifier.TriggerSide.Right && lastTrigger != LastTrigger.Right))
        {
            lastTrigger = side == WallTriggerNotifier.TriggerSide.Left ? LastTrigger.Left : LastTrigger.Right;
            lastTriggerTimestamp = Time.time;
            isFading = false;

            BrightenWall();
            SpawnVFX(vfxPosition);
            Debug.Log($"Trigger {side} activated.");
        }
    }

    void BrightenWall()
    {
        currentBrightness += brightenAmount;
        currentBrightness = Mathf.Clamp(currentBrightness, 0f, maxBrightness);
        UpdateMaterialBrightness();

        if (!hasReachedMax && currentBrightness >= maxBrightness)
        {
            hasReachedMax = true;
            SpawnMaxBrightnessVFX(transform.position); // Or a specified VFX spawn location
        }
    }

    void UpdateMaterialBrightness()
    {
        wallRenderer.GetPropertyBlock(mpb);
        Color newColor = Color.Lerp(initialColor, targetColor, currentBrightness);
        mpb.SetColor(BaseColor, newColor);
        wallRenderer.SetPropertyBlock(mpb);
    }

    void SpawnVFX(Vector3 position)
    {
        if (vfxPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxPrefab, position, Quaternion.identity);
            vfxInstance.transform.localScale = vfxScale;
            Destroy(vfxInstance, vfxLifetime);
        }
    }

    void SpawnMaxBrightnessVFX(Vector3 position)
    {
        if (vfxMaxBrightnessPrefab != null)
        {
            GameObject vfxInstance = Instantiate(vfxMaxBrightnessPrefab, position, Quaternion.identity);
            vfxInstance.transform.localScale = vfxMaxBrightnessScale;
            Destroy(vfxInstance, vfxMaxBrightnessLifetime);
        }
    }


}
