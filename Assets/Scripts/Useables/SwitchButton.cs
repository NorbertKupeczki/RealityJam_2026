using DG.Tweening;
using UnityEngine;

public class SwitchButton : Usable
{
    [Header("Switch")]
    [SerializeField] private Transform m_Switch;
    [Header("Terminal ID")]
    [SerializeField] private uint m_TerminalId;

    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;

    private float m_MoveDistance = 0.25f;

    private bool m_IsOn;
    private bool m_CanBeInteracted;

    private const float ANIMATION_DURATION = 0.1f;

    private void Awake()
    {
        OnUse += HandleOnUse;
        m_IsOn = true;
        m_CanBeInteracted = true;

        m_StartPosition = m_Switch.transform.position;
        m_EndPosition = m_StartPosition + Vector3.down * m_MoveDistance;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnDestroy()
    {        
        OnUse -= HandleOnUse;
    }
    
    private void HandleOnUse()
    {
        if (!m_CanBeInteracted) { return; }

        m_IsOn = !m_IsOn;
        m_CanBeInteracted = false;

        m_Switch.transform.DOMove(m_IsOn? m_StartPosition : m_EndPosition, ANIMATION_DURATION)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => { m_CanBeInteracted = true; }
            );

        if (!m_IsOn) { return; }
        ChallengeManager.TerminalIsReset(m_TerminalId);
    }    
}
