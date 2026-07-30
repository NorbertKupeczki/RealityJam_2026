using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public event Action<float> OnSliderValueChanged;
    
    [SerializeField] private Slider m_Slider;
    [SerializeField] private TMP_Text m_SliderValueText;
    
    private const float DEFAULT_VOLUME_VALUE = 5.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_Slider.onValueChanged.AddListener(UpdateSliderValueText);
        UpdateSliderValueText(m_Slider.value);
    }

    private void OnDestroy()
    {
        m_Slider.onValueChanged.RemoveListener(UpdateSliderValueText);
    }

    private void UpdateSliderValueText(float value)
    {
        m_SliderValueText.text = Mathf.RoundToInt(value).ToString();
        OnSliderValueChanged?.Invoke(value);

        if (isActiveAndEnabled)
        {
            //AudioManager.Instance.PlayOneShotAudio(
            //    AudioLibrary.Instance.UiSliderValueChange,
            //    Camera.main.transform.position);
        }
    }
    
    public float GetSliderValue() => m_Slider.value;

    public void LoadVolume(string prefKey)
    {
        m_Slider.value = PlayerPrefsManager.Instance.LoadFloat(prefKey, out float musicVolume)
            ? musicVolume
            : DEFAULT_VOLUME_VALUE;
    }
}
