using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private CanvasGroup m_GameOverText;
    [SerializeField] private CanvasGroup m_YouLetTheMainframeBreakText;
    [SerializeField] private Button m_ExitButton;
    [SerializeField] private Button m_TryAgainButton;

    private void Start()
    {
        m_ExitButton.onClick.AddListener(HandleMainMenuButton);
        m_TryAgainButton.onClick.AddListener(HandleTryAgainButton);
        m_CanvasGroup.alpha = 0.0f;
        m_GameOverText.alpha = 0.0f;
        m_YouLetTheMainframeBreakText.alpha = 0.0f;
        m_ExitButton.gameObject.SetActive(false);
        m_TryAgainButton.gameObject.SetActive(false);
        
        gameObject.SetActive(false);
    }
    
    public void StartGameOverSequence()
    {
        gameObject.SetActive(true);
        
        m_CanvasGroup.DOFade(1.0f, 1.5f)
            .OnComplete(() =>
            {
                ShowGameOverText();
            });
    }

    private void ShowGameOverText()
    {
        m_GameOverText.DOFade(1.0f, 1.5f)
            .SetDelay(1.0f)
            .OnComplete(() =>
            {
                ShowMainframeBrokenText();
            });
    }

    private void ShowMainframeBrokenText()
    {
        m_YouLetTheMainframeBreakText.DOFade(1.0f, 1.5f)
            .SetDelay(1.0f)
            .OnComplete(() =>
            {
                m_ExitButton.gameObject.SetActive(true);
                m_TryAgainButton.gameObject.SetActive(true);
                m_ExitButton.Select();
            });
    }
    
    private void HandleMainMenuButton()
    {
        Time.timeScale = 1;
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }

    private void HandleTryAgainButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    
}
