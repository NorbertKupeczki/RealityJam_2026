using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button m_StartGameButton;
    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Button m_QuitButton;

    [Header(" Button Groups")]
    [SerializeField] private GameObject m_MainMenuButtonContainer;
    [SerializeField] private SettingsPanel m_SettingsPanel;

    private void Awake()
    {
        m_StartGameButton.onClick.AddListener(HandleStartButtonPressed);
        m_SettingsButton.onClick.AddListener(HandleSettingsButtonPressed);
        m_QuitButton.onClick.AddListener(HandleQuitButtonPressed);

        m_SettingsPanel.OnSettingsPanelClose += HandleCloseSettingsPanel;
    }

    private void Start()
    {
        InputManager.Instance.SwitchToInputMap(InputManager.InputMaps.UI);
#if !UNITY_EDITOR
        InputManager.Instance.ToggleLockCursor(true);
#endif
        
        m_StartGameButton.Select();
        ToggleSettingsPanel(false, false);
    }

    private void OnDestroy()
    {
        m_StartGameButton.onClick.RemoveAllListeners();
        m_SettingsButton.onClick.RemoveAllListeners();
        m_QuitButton.onClick.RemoveAllListeners();
        
        m_SettingsPanel.OnSettingsPanelClose -= HandleCloseSettingsPanel;
    }

    private void HandleStartButtonPressed()
    {
        PlaySubmitAudio();
        Loader.LoadScene(Loader.Scenes.GameScene);
    }

    private void HandleSettingsButtonPressed()
    {
        PlaySubmitAudio();
        ToggleSettingsPanel(true);
    }

    private void HandleQuitButtonPressed()
    {
        PlaySubmitAudio();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ToggleSettingsPanel(bool toggle, bool selectSettingsButton = true)
    {
        m_MainMenuButtonContainer.SetActive(!toggle);
        m_SettingsPanel.gameObject.SetActive(toggle);

        if (!toggle && selectSettingsButton)
        {
            m_SettingsButton.Select();
        }
    }

    private void HandleCloseSettingsPanel()
    {
        ToggleSettingsPanel(false);
    }

    private void PlaySubmitAudio()
    {
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.UiSubmit,
        //    Camera.main.transform.position);
    }
}
