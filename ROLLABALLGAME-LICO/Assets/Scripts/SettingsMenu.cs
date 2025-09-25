using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public Toggle muteToggle;
    public Slider brightnessSlider;

    [Header("UI Labels")]
    public TMP_Text volumeLabel;
    public TMP_Text brightnessLabel;

    [Header("Audio")]
    public AudioMixer audioMixer;

    private float lastVolume = 1f;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        bool muted = PlayerPrefs.GetInt("Muted", 0) == 1;
        float brightness = PlayerPrefs.GetFloat("Brightness", 1f);

        volumeSlider.value = volume;
        muteToggle.isOn = muted;
        brightnessSlider.value = brightness;

        ApplyVolume(volume);
        ApplyMute(muted);
        ApplyBrightness(brightness);

        volumeSlider.onValueChanged.AddListener(ApplyVolume);
        muteToggle.onValueChanged.AddListener(ApplyMute);
        brightnessSlider.onValueChanged.AddListener(ApplyBrightness);
    }

    public void ApplyVolume(float value)
    {
        if (!muteToggle.isOn)
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
            lastVolume = value;
        }

        if (volumeLabel != null)
            volumeLabel.text = "Volume: " + Mathf.RoundToInt(value * 100) + "%";

        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    public void ApplyMute(bool muted)
    {
        if (muted)
        {
            audioMixer.SetFloat("Volume", -80f);
            PlayerPrefs.SetInt("Muted", 1);

            if (volumeLabel != null)
                volumeLabel.text = "Volume: Muted";
        }
        else
        {
            ApplyVolume(lastVolume);
            PlayerPrefs.SetInt("Muted", 0);
        }
        PlayerPrefs.Save();
    }

    public void ApplyBrightness(float value)
    {
        RenderSettings.ambientLight = Color.white * value;

        if (brightnessLabel != null)
            brightnessLabel.text = "Brightness: " + Mathf.RoundToInt(value * 100) + "%";

        PlayerPrefs.SetFloat("Brightness", value);
        PlayerPrefs.Save();
    }
}
