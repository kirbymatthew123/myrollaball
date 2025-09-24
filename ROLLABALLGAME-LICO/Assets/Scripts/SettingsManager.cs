using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [Header("Brightness")]
    public Image brightnessOverlay;
    public float overlayMaxAlpha = 0.7f;
    public float overlayMinAlpha = 0f;

    [Header("Audio")]
    public AudioSource bgMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        ApplyBrightness(savedBrightness, 0.2f, 1.5f, null);

        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        ApplyVolume(savedVolume, null);

        bool muted = PlayerPrefs.GetInt("Muted", 0) == 1;
        SetMute(muted);
    }

    public void ApplyBrightness(float value, float min, float max, TMP_Text valueText)
    {
        float normalized = (value - min) / (max - min);

        if (brightnessOverlay != null)
        {
            float alpha = Mathf.Lerp(overlayMaxAlpha, overlayMinAlpha, normalized);
            Color c = brightnessOverlay.color;
            c.a = alpha;
            brightnessOverlay.color = c;
        }

        if (valueText != null)
            valueText.text = Mathf.RoundToInt(normalized * 100f) + "%";

        PlayerPrefs.SetFloat("Brightness", value);
    }

    public void ApplyVolume(float value, TMP_Text valueText)
    {
        if (bgMusic != null && !bgMusic.mute)
            bgMusic.volume = value;

        if (valueText != null)
            valueText.text = Mathf.RoundToInt(value * 100f) + "%";

        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetMute(bool isMuted)
    {
        if (bgMusic != null)
            bgMusic.mute = isMuted;

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }
}
