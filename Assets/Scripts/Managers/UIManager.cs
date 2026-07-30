using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TMP_Text m_InteractionText;
    [SerializeField] private TMP_Text m_AuxiliaryText;
    [SerializeField] private TMP_Text m_BatteryChargeText;
    [SerializeField] private TMP_Text m_BatteryStatusText;
    
    [Header("Pause Menu")]
    [SerializeField] private Transform m_PauseMenu;
    [SerializeField] private Button m_ResumeButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_MainMenuButton;
    
    [Header("Settings Panel")]
    [SerializeField] private SettingsPanel m_SettingsPanel;
    
    [Header("Game Over Panel")]
    [SerializeField] private GameOverPanel m_GameOverPanel;

    private const string INTERACTION_PICKUP = "Pick up <color=#00ffffff><b>(F)";
    private const string INTERACTION_DRAIN = "Drain <color=#00ffffff><b>(E)";
    private const string INTERACTION_CHARGE = "Charge <color=#00ffffff><b>(E)";
    private const string INTERACTION_INSERT = "Insert <color=#00ffffff><b>(E)";
    private const string INTERACTION_USE = "Use <color=#00ffffff><b>(E)";
    
    protected override void Awake()
    {
        base.Awake();
        
        ToggleInteractionText(false, GameEnums.InteractionType.None);
        m_AuxiliaryText.gameObject.SetActive(false);
    }

    private void Start()
    {
        PlayerManager.Instance.OnInteractableSelected += ToggleInteractionText;
        InputManager.Instance.OnMenuPressed += HandleMenuButtonPressed;
        
        // Button onClick subscriptions
        m_ResumeButton.onClick.AddListener(HandleResumeButton);
        m_SettingsButton.onClick.AddListener(HandleSettingsButton);
        m_MainMenuButton.onClick.AddListener(HandleMainMenuButton);
        
        m_SettingsPanel.OnSettingsPanelClose += HandleSettingsPanelClose;
        
        PlayerManager.Instance.GetBattery.OnBatteryChargeChanged += HandleBatteryChargeChange;
        PlayerManager.Instance.GetBattery.OnBatteryIsFlat += HandleBatteryOutOfCharge;
        
        m_PauseMenu.gameObject.SetActive(false);
        m_SettingsPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnInteractableSelected -= ToggleInteractionText;
        InputManager.Instance.OnMenuPressed -= HandleMenuButtonPressed;
        
        m_ResumeButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_MainMenuButton.onClick.RemoveAllListeners();
        
        m_SettingsPanel.OnSettingsPanelClose -= HandleSettingsPanelClose;
        
        PlayerManager.Instance.GetBattery.OnBatteryChargeChanged -= HandleBatteryChargeChange;
        PlayerManager.Instance.GetBattery.OnBatteryIsFlat -= HandleBatteryOutOfCharge;
    }
    
    private void ToggleInteractionText(bool toggle, GameEnums.InteractionType interactionType)
    {
        m_InteractionText.gameObject.SetActive(toggle);

        m_InteractionText.text = interactionType switch
        {
            GameEnums.InteractionType.None => "",
            GameEnums.InteractionType.Pickup => INTERACTION_PICKUP,
            GameEnums.InteractionType.Drain  => INTERACTION_DRAIN,
            GameEnums.InteractionType.Charge => INTERACTION_CHARGE,
            GameEnums.InteractionType.Insert => INTERACTION_INSERT,
            GameEnums.InteractionType.Use => INTERACTION_USE,
            _ => throw new ArgumentOutOfRangeException(nameof(interactionType), interactionType, null)
        };
    }

    public void ToggleAuxiliaryText(bool toggle, string text = "")
    {
        m_AuxiliaryText.gameObject.SetActive(toggle);

        if (toggle)
        {
            m_AuxiliaryText.text = text;
        }
    }

    private void HandleMenuButtonPressed()
    {
        TogglePauseGame(true);
    }

    private void TogglePauseGame(bool pause)
    {
        // Stop/resume time
        Time.timeScale = pause ? 0 : 1;
        
        // Switch Action maps
        InputManager.Instance.SwitchToInputMap(pause? InputManager.InputMaps.UI : InputManager.InputMaps.Game);

        // Show UI element
        m_PauseMenu.gameObject.SetActive(pause);

        if (!pause) { return; }
        
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.UiPanelOpen,
        //    Camera.main.transform.position);
        m_ResumeButton.Select();
    }
    
    private void HandleResumeButton()
    {
        PlaySubmitAudio();
        TogglePauseGame(false);
    }

    private void HandleSettingsButton()
    {
        PlaySubmitAudio();
        m_PauseMenu.gameObject.SetActive(false);
        ToggleSettingsPanel(true);
    }

    private void HandleMainMenuButton()
    {
        PlaySubmitAudio();
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }

    private void HandleSettingsPanelClose()
    {
        ToggleSettingsPanel(false);
    }
    
    private void ToggleSettingsPanel(bool toggle)
    {
        m_SettingsPanel.gameObject.SetActive(toggle);

        if (toggle)
        {
            //AudioManager.Instance.PlayOneShotAudio(
            //    AudioLibrary.Instance.UiPanelOpen,
            //    Camera.main.transform.position);
            return;
        }
        
        m_PauseMenu.gameObject.SetActive(true);
        m_SettingsButton.Select();
    }

    private void HandleBatteryChargeChange(float value)
    {
        m_BatteryChargeText.text = (100.0f * value).ToString("F2");

        m_BatteryStatusText.text = value switch
        {
            < 0.1f => "<color=red>CRITICAL!</color>",
            < 0.3f => "<color=yellow>WARNING!</color>",
            > 0.3f => "<color=green>OK</color>",
            _ => ""
        };
    }

    private void HandleBatteryOutOfCharge()
    {
        InputManager.Instance.SwitchToInputMap(InputManager.InputMaps.UI);
        m_GameOverPanel.StartGameOverSequence();
    }

    private void PlaySubmitAudio()
    {
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.UiSubmit,
        //    Camera.main.transform.position);
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            HandleBatteryOutOfCharge();
        }
    }
#endif
}
