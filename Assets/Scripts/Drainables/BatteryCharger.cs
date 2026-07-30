using UnityEngine;

public class BatteryCharger : Drainable
{
    [Header("Battery Charger")]
    [SerializeField] private uint m_AmountOfCharges;

    [Header("Particles")]
    [SerializeField] private ParticleSystem m_Particles;

    private const string DRAINED = "CHARGER DEPLETED"; 
    
    public override uint DrainPower()
    {
        if (m_AmountOfCharges <= 0) { return 0; }
        
        m_AmountOfCharges--;
        if (m_AmountOfCharges == 0)
        {
            m_Particles.Stop();
            m_IsDrainable = false;
        }
        
        TurnOnAuxiliaryText();
        
        return m_DrainableAmount;
    }

    public override void MarkObject()
    {
        TurnOnAuxiliaryText();
    }

    public override void UnmarkObject()
    {
        UIManager.Instance.ToggleAuxiliaryText(false);
    }

    private void TurnOnAuxiliaryText()
    {
        UIManager.Instance.ToggleAuxiliaryText(
            true,
            m_IsDrainable?
                $"Drain <color=#00ffffff><b>{m_DrainableAmount}%</b></color> power" +
                $"\n<color=#00ffffff><b>{m_AmountOfCharges}</b></color> charges left!" :
                DRAINED);
    }
}
