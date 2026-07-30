using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public event Action OnSettingsPanelClose;
    
    [SerializeField] private VolumeSlider m_MusicVolumeSlider;
    [SerializeField] private VolumeSlider m_EffectsVolumeSlider;
    [SerializeField] private Button m_BackButton;
    
    private const string MUSIC_VOLUME_PREF = "MUSIC_VOLUME";
    private const string EFFECTS_VOLUME_PREF = "EFFECTS_VOLUME";

    private void Awake()
    {
        m_MusicVolumeSlider.OnSliderValueChanged += HandleMusicSliderValueChanged;
        m_EffectsVolumeSlider.OnSliderValueChanged += HandleEffectsVolumeSliderValueChanged;
        m_BackButton.onClick.AddListener(HandleBackButtonPressed);
    }

    private void OnEnable()
    {
        LoadValues();
        m_BackButton.Select();
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.UiPanelOpen,
        //    Camera.main.transform.position);
    }

    private void OnDestroy()
    {
        m_MusicVolumeSlider.OnSliderValueChanged -= HandleMusicSliderValueChanged;
        m_EffectsVolumeSlider.OnSliderValueChanged -= HandleEffectsVolumeSliderValueChanged;
        m_BackButton.onClick.RemoveListener(HandleBackButtonPressed);
    }

    private void HandleMusicSliderValueChanged(float value)
    {
        //Debug.Log($"Music slider value: {value}");
        //AudioManager.Instance.SetMusicVolume(value * 0.1f);
    }

    private void HandleEffectsVolumeSliderValueChanged(float value)
    {
        //Debug.Log($"Effect slider value: {value}");
        //AudioManager.Instance.SetEffectsVolume(value * 0.1f);
    }
    
    private void HandleBackButtonPressed()
    {
        SaveValues();
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.UiSubmit,
        //    Camera.main.transform.position);
        OnSettingsPanelClose?.Invoke();
    }

    private void LoadValues()
    {
        m_MusicVolumeSlider.LoadVolume(MUSIC_VOLUME_PREF);
        m_EffectsVolumeSlider.LoadVolume(EFFECTS_VOLUME_PREF);
    }

    private void SaveValues()
    {
        PlayerPrefsManager.Instance.SaveFloat(MUSIC_VOLUME_PREF, m_MusicVolumeSlider.GetSliderValue());
        PlayerPrefsManager.Instance.SaveFloat(EFFECTS_VOLUME_PREF, m_EffectsVolumeSlider.GetSliderValue());
        PlayerPrefs.Save();
    }
}
