using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarElement : MonoBehaviour
{
    [SerializeField] private TMP_Text m_ProgressText;
    [SerializeField] private Image m_LoadingBarFillImage;

    private void Start()
    {
        Loader.Instance.OnLoadProgressChanged += HandleLoadingProgressChange;
        HandleLoadingProgressChange(0.0f);
    }

    private void OnDestroy()
    {
        Loader.Instance.OnLoadProgressChanged -= HandleLoadingProgressChange;
    }
    
    private void HandleLoadingProgressChange(float progress)
    {
        m_LoadingBarFillImage.fillAmount = progress;
        m_ProgressText.text = progress.ToString("P");
    }
}
