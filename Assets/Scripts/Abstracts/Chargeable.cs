using System;
using UnityEngine;

public abstract class Chargeable : MonoBehaviour, IInteractable
{
    public event Action<float> OnChargeLevelChanged;
    public event Action OnObjectFullyCharged;
    
    [Header("Chargeable")]
    [SerializeField, Range(25, 100)] protected uint m_MaxCharge;
    protected uint m_CurrentCharge;
    protected bool m_IsFullyCharged;
    
    protected Collider m_Collider;
    
    public static string FULLY_CHARGED = "FULLY CHARGED";

    public uint ChargeNeededToMax => m_MaxCharge - m_CurrentCharge;
    public float GetChargeLevelNormalised => (float)m_CurrentCharge / m_MaxCharge;
    public bool IsFullyCharged => m_IsFullyCharged;
    
    protected virtual void Awake()
    {
        if (!TryGetComponent<Collider>(out m_Collider))
        {
            Debug.LogError($"Collectable {gameObject.name} is missing a Collider!");
        }
    }

    public virtual void ChargeObject(uint value)
    {
        if (m_IsFullyCharged) { return; }

        m_CurrentCharge = (uint)Mathf.Clamp(m_CurrentCharge + value, 0, m_MaxCharge);
        OnChargeLevelChanged?.Invoke(GetChargeLevelNormalised);

        if (m_CurrentCharge != m_MaxCharge) { return; }
        m_IsFullyCharged = true;
        OnObjectFullyCharged?.Invoke();
    }

    public abstract void MarkObject();

    public abstract void UnmarkObject();

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Charge;
    
    public GameObject InteractableGameObject => gameObject;
}
