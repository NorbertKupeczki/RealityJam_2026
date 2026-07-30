using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private CanvasGroup m_GameOverText;
    [SerializeField] private CanvasGroup m_OutOfBatteryText;
    [SerializeField] private Button m_ExitButton;

    private void Start()
    {
        m_ExitButton.onClick.AddListener(HandleExitButton);
        m_CanvasGroup.alpha = 0.0f;
        m_GameOverText.alpha = 0.0f;
        m_OutOfBatteryText.alpha = 0.0f;
        m_ExitButton.gameObject.SetActive(false);
        
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
                ShowOutOfBatteryText();
            });
    }

    private void ShowOutOfBatteryText()
    {
        m_OutOfBatteryText.DOFade(1.0f, 1.5f)
            .SetDelay(1.0f)
            .OnComplete(() =>
            {
                m_ExitButton.gameObject.SetActive(true);
                m_ExitButton.Select();
            });
    }
    
    private void HandleExitButton()
    {
        Time.timeScale = 1;
        Loader.LoadScene(Loader.Scenes.MainMenu);
    }
    
}
