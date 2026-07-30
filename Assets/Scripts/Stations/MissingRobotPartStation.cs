using System;
using UnityEngine;

public class MissingRobotPartStation : Station
{
    public event Action<ItemDataSO> OnPartDeposited;
    
    [SerializeField] private Material m_RobotPartMaterial;
    [SerializeField] private SkinnedMeshRenderer m_RobotPartRenderer;
    
    public ItemDataSO DepositableItems => m_DepositableItems;
    private bool m_PartDeposited = false;
    
    public override bool DepositItem(Collectable item)
    {
        if (item.ItemData.CollectableType != m_DepositableItems.CollectableType) { return false; }
        
        Destroy(item.gameObject);
        
        m_RobotPartRenderer.material = m_RobotPartMaterial;
        
        OnPartDeposited?.Invoke(item.ItemData);
        m_PartDeposited = true;
        HandleItemDeposited();
        return true;
    }

    public override void MarkObject()
    {
        if (m_PartDeposited) { return; }
        UIManager.Instance.ToggleAuxiliaryText(true,$"Needs a {m_DepositableItems.ItemName}");
    }

    public override void UnmarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(false);
    }
}
