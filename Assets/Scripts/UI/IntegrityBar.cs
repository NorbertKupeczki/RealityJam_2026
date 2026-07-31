using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntegrityBar : MonoBehaviour
{
    [SerializeField] private TMP_Text m_IntegrityText;
    [SerializeField] private Image m_IntegrityBarFillImage;

    private void Start()
    {
        ChallengeManager.OnSystemIntegrityChanged += HandleLoadingProgressChange;
        HandleLoadingProgressChange(0.0f);
    }

    private void OnDestroy()
    {
        ChallengeManager.OnSystemIntegrityChanged -= HandleLoadingProgressChange;
    }

    private void HandleLoadingProgressChange(float progress)
    {
        m_IntegrityBarFillImage.fillAmount = progress;
        m_IntegrityText.text = progress.ToString("P");
    }
}
