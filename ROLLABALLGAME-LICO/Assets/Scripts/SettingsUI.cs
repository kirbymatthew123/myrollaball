using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    public Slider brightnessSlider;
    public Slider volumeSlider;
    public Toggle muteToggle;
    public Button backButton;
    public TMP_Text brightnessValueText;
    public TMP_Text volumeValueText;

    void Start()
    {
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteChanged);
        backButton.onClick.AddListener(BackToMenu);

        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1;
    }

    public void OnBrightnessChanged(float value)
    {
        SettingsManager.Instance.ApplyBrightness(
            value,
            brightnessSlider.minValue,
            brightnessSlider.maxValue,
            brightnessValueText
        );
    }

    public void OnVolumeChanged(float value)
    {
        SettingsManager.Instance.ApplyVolume(value, volumeValueText);
    }

    public void OnMuteChanged(bool isMuted)
    {
        SettingsManager.Instance.SetMute(isMuted);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
