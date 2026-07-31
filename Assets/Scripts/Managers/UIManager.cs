using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoSingleton<UIManager>
{
    
    [Header("Game Over Panel")]
    [SerializeField] private GameOverPanel m_GameOverPanel;
    
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
            HandleMainframeBroken();
        }
    }
#endif
}
