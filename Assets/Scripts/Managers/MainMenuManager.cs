using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button m_StartGameButton;
    [SerializeField] private Button m_QuitButton;

    private void Awake()
    {
        m_StartGameButton.onClick.AddListener(HandleStartButtonPressed);
        m_QuitButton.onClick.AddListener(HandleQuitButtonPressed);
    }

    private void Start()
    {
        InputManager.Instance.SwitchToInputMap(InputManager.InputMaps.UI);        
        m_StartGameButton.Select();
    }

    private void OnDestroy()
    {
        m_StartGameButton.onClick.RemoveAllListeners();
        m_QuitButton.onClick.RemoveAllListeners();
    }

    private void HandleStartButtonPressed()
    {
        PlaySubmitAudio();
        Loader.LoadScene(Loader.Scenes.GameScene);
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

    private void PlaySubmitAudio()
    {
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.UiSubmit,
        //    Camera.main.transform.position);
    }
}
