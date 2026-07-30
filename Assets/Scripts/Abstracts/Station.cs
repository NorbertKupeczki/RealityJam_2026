using System;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class Station : MonoBehaviour, IInteractable
{
    public event Action OnItemDeposited;
    
    [SerializeField] protected ItemDataSO m_DepositableItems;
    [SerializeField] protected Transform m_DepositableItemParent;
    
    protected Collectable m_DepositedItem = null;
    
    protected virtual void Awake()
    {
        Assert.IsNotNull(m_DepositableItems, $"{name} >> DepositableItems cannot be null.");
        Assert.IsNotNull(m_DepositableItemParent, $"{name} >> m_DepositableItemParent cannot be null.");
    }

    protected virtual void Start()
    {
        
    }

    protected void HandleItemDeposited()
    {
        OnItemDeposited?.Invoke();
    }

    public abstract bool DepositItem(Collectable item);

    public abstract void MarkObject();

    public abstract void UnmarkObject();

    public bool CanItemBeDeposited(Collectable item)
    {
        return m_DepositableItems == item.ItemData;
    }

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Insert;
    
    public GameObject InteractableGameObject => gameObject;
}
