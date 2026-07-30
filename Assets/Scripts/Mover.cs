using DG.Tweening;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header ("---- USE ONLY ONE TRIGGER ----")]
    [SerializeField] private Usable m_TriggerUsable;
    [SerializeField] private Station m_TriggerStation;
    [SerializeField] private Chargeable m_TriggerChargeable;

    [Header("Moving object")]
    [SerializeField] private Transform m_ObjectToMove;
    [SerializeField] private Vector3 m_MoveVector;
    
    private Vector3 m_OriginalPosition;
    private Vector3 m_TargetPosition;
    
    private bool m_IsMoving;
    private bool m_IsAtTargetPosition;

    private const float TWEEN_DURATION = 1.5f;

    private void Awake()
    {
        m_IsMoving = false;
        m_IsAtTargetPosition = false;
        
        m_OriginalPosition = m_ObjectToMove.localPosition;
        m_TargetPosition = m_OriginalPosition + m_MoveVector;
    }

    private void Start()
    {
        if (m_TriggerUsable)
        {
            m_TriggerUsable.OnUse += HandleMoveToggle;
        }
        else if (m_TriggerStation)
        {
            m_TriggerStation.OnItemDeposited += MoveToTarget;
        }
        else if (m_TriggerChargeable)
        {
            m_TriggerChargeable.OnObjectFullyCharged += MoveToTarget;
        }
    }

    private void OnDestroy()
    {
        if (m_TriggerUsable)
        {
            m_TriggerUsable.OnUse -= HandleMoveToggle;
        }
        else if (m_TriggerStation)
        {
            m_TriggerStation.OnItemDeposited -= MoveToTarget;
        }
        else if (m_TriggerChargeable)
        {
            m_TriggerChargeable.OnObjectFullyCharged -= MoveToTarget;
        }
    }
    
    private void HandleMoveToggle()
    {
        if (m_IsAtTargetPosition)
        {
            MoveToOrigin();
        }
        else
        {
            MoveToTarget();
        }
    }
    
    private void MoveToTarget()
    {
        if (m_IsMoving || m_IsAtTargetPosition) { return; }
        m_IsMoving = true;
        
        m_ObjectToMove.DOLocalMove(m_TargetPosition,TWEEN_DURATION)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                m_IsMoving = false;
                m_IsAtTargetPosition = true;
            });
    }

    private void MoveToOrigin()
    {
        if (m_IsMoving || !m_IsAtTargetPosition) { return; }
        m_IsMoving = true;
        
        m_ObjectToMove.DOLocalMove(m_OriginalPosition,TWEEN_DURATION)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                m_IsMoving = false;
                m_IsAtTargetPosition = false;
            });
    }
}
