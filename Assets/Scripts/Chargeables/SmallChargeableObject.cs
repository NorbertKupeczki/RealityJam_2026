using UnityEngine;

public class SmallChargeableObject : Chargeable
{
    public override void MarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(true,
            m_IsFullyCharged?
                FULLY_CHARGED:
                $"TRANSFER <b><color=#00ffffff>{ChargeNeededToMax}</color></b> ENERGY TO CHARGE");
    }

    public override void UnmarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(false);
    }
}
