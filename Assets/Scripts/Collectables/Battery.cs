using UnityEngine;

public class Battery : Collectable
{
    protected override void Start()
    {
        base.Start();
    }
    
    public override void Collect(Transform parent)
    {
        base.Collect(parent);
    }

    public override void Drop(Vector3 dropPosition)
    {
        base.Drop(dropPosition);
    }

    public override void Use()
    {
        Debug.Log("Battery used");
    }

    public override void MarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(true, m_ItemData.ItemName);
    }

    public override void UnmarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(false);
    }
}
