using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainframeScreenUI : MonoBehaviour
{
    [SerializeField] private TMP_Text m_SuccessText;
    [SerializeField] private TMP_Text m_FailureText;
    [SerializeField] private TMP_Text m_SequenceText;

    private const string SEQUENCE_TEXT_START = "Please reset terminal(s):\r\n<b><size=150%>";
    private const string SEQUENCE_TEXT_END = "</b></size>";

    private CanvasGroup m_SuccessCG;
    private CanvasGroup m_FailureCG;
    private CanvasGroup m_SequenceCG;

    private const float ANIMATION_DURATION = 0.2f;
    private const float SUCCESS_DISPLAY_DURATION = 1.0f;
    private const float FAILURE_DISPLAY_DURATION = 4.0f;

    private void Awake()
    {
        m_SuccessCG = m_SuccessText.gameObject.GetComponent<CanvasGroup>();
        m_FailureCG = m_FailureText.gameObject.GetComponent<CanvasGroup>();
        m_SequenceCG = m_SequenceText.gameObject.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ResetAllCanvasGroups();

        ChallengeManager.OnNewChallenge += HandleNewChallenge;
        ChallengeManager.OnChallengeSuccessful += HandleChallengeSuccessful;
        ChallengeManager.OnTermialReset += HandleChallengeFailed;
    }

    private void OnDestroy()
    {
        ChallengeManager.OnNewChallenge -= HandleNewChallenge;
        ChallengeManager.OnChallengeSuccessful -= HandleChallengeSuccessful;
        ChallengeManager.OnTermialReset -= HandleChallengeFailed;
    }

    private void HandleNewChallenge(string sequenceAsString)
    {
        GenerateSequenceString(sequenceAsString);
        DisplayTextElement(m_SequenceCG, false);
    }

    private void HandleChallengeSuccessful()
    {
        DisplayTextElement(m_SuccessCG);
    }

    private void HandleChallengeFailed(bool success)
    {
        if (success) { return; }
        DisplayTextElement(m_FailureCG);
    }

    private void GenerateSequenceString(string sequenceAsString)
    {
        m_SequenceText.text = $"{SEQUENCE_TEXT_START}{sequenceAsString}{SEQUENCE_TEXT_END}";
    }

    private void DisplayTextElement(CanvasGroup canvasGroup, bool fadeOut = true)
    {
        ResetAllCanvasGroups();

        canvasGroup.DOFade(1.0f, ANIMATION_DURATION)
            .SetEase(Ease.InOutSine)            
            .OnComplete(() => 
                {
                    if (fadeOut)
                    {
                        canvasGroup.DOFade(0.0f, ANIMATION_DURATION)
                            .SetEase(Ease.InOutSine)
                            .SetDelay(canvasGroup == m_SuccessCG? SUCCESS_DISPLAY_DURATION : FAILURE_DISPLAY_DURATION);
                    }
                });
    }

    private void ResetAllCanvasGroups()
    {
        m_SuccessCG.alpha = 0.0f;
        m_FailureCG.alpha = 0.0f;
        m_SequenceCG.alpha = 0.0f;
    }

}
