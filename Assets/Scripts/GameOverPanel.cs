using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private CanvasGroup m_GameOverText;
    [SerializeField] private CanvasGroup m_YouLetTheMainframeBreakText;

    private bool m_IsGameOver;

    private void Start()
    {
        m_CanvasGroup.alpha = 0.0f;
        m_GameOverText.alpha = 0.0f;
        m_YouLetTheMainframeBreakText.alpha = 0.0f;

        m_IsGameOver = false;
        gameObject.SetActive(false);
    }
    
    public void StartGameOverSequence()
    {
        if(m_IsGameOver) { return; }
        m_IsGameOver = true;

        Debug.Log("GameOver");
        gameObject.SetActive(true);
        
        m_CanvasGroup.DOFade(1.0f, 1.5f)
            .OnComplete(() =>
            {
                m_CanvasGroup.interactable = true;
                m_CanvasGroup.blocksRaycasts = true;
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
                m_GameOverText.DOFade(1.0f, 5.0f).OnComplete(BackToMainMenu);
            });
    }
    
    private void BackToMainMenu()
    {
        Debug.Log("BackToMain");
        Time.timeScale = 1;
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }
    
}
