using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoSingleton<UIManager>
{
    
    [Header("Game Over Panel")]
    [SerializeField] private GameOverPanel m_GameOverPanel;
    [SerializeField] private GameOverPanel m_SecretWinPanel;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ChallengeManager.OnMainframeBroken += HandleMainframeBroken;        
    }

    private void OnDestroy()
    {
        ChallengeManager.OnMainframeBroken -= HandleMainframeBroken;
    }

    private void HandleMainMenuButton()
    {
        PlaySubmitAudio();
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }

    private void HandleMainframeBroken()
    {
        InputManager.Instance.SwitchToInputMap(InputManager.InputMaps.UI);
                
        m_GameOverPanel.StartGameOverSequence();
    }

    private void HandleSecretWinScreen()
    {
        InputManager.Instance.SwitchToInputMap(InputManager.InputMaps.UI);

        m_SecretWinPanel.StartGameOverSequence();
    }

    private void PlaySubmitAudio()
    {
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.UiSubmit,
        //    Camera.main.transform.position);
    }


    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            HandleSecretWinScreen();
        }
    }

}
