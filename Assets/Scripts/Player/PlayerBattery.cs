using System;
using System.Collections;
using UnityEngine;

public class PlayerBattery : MonoBehaviour
{
    private enum BatteryStatus
    {
        OK = 0,
        Low,
        Critical
    }
    
    public event Action<float> OnBatteryChargeChanged;
    public event Action OnBatteryIsFlat;

    private const float MAX_BATTERY_CHARGE = 1.0f;
    private const float DRAIN_INTERVAL = 0.1f;
    private const float DRAIN_RATE = -0.001f;
    
    private float m_BatteryLevel;
    private BatteryStatus m_BatteryStatus = BatteryStatus.OK;
    
    private Coroutine m_AutoDrainBatteryRoutine;

    public bool HasBatteryCharge(uint charge) => m_BatteryLevel > charge * 0.01f;

    private void Start()
    {
        m_BatteryLevel = MAX_BATTERY_CHARGE;
        
        m_AutoDrainBatteryRoutine = StartCoroutine(AutoDrainBattery());
    }
    
    public void ChangeBatteryCharge(float amount)
    {
        m_BatteryLevel = Mathf.Clamp(m_BatteryLevel + amount, 0.0f, MAX_BATTERY_CHARGE);
        OnBatteryChargeChanged?.Invoke(m_BatteryLevel);

        switch (m_BatteryLevel)
        {
            case <= 0.0f:
            {
                TriggerBatteryIsOutOfCharge();
                break;
            }
            case < 0.1f when m_BatteryStatus != BatteryStatus.Critical:
            {
                //Debug.Log("Critical Battery");
                m_BatteryStatus = BatteryStatus.Critical;
                break;
            }
            case < 0.3f when m_BatteryStatus != BatteryStatus.Low:
            {
                if (m_BatteryStatus == BatteryStatus.Critical && amount < 0) { break; }
                //Debug.Log("Low Battery");
                if (m_BatteryStatus == BatteryStatus.OK)
                {
                        // AUDIO...
                }
                m_BatteryStatus = BatteryStatus.Low;
                break;
            }
            case >= 0.3f when m_BatteryStatus != BatteryStatus.OK:
            {
                //Debug.Log("OK Battery");
                m_BatteryStatus = BatteryStatus.OK;
                break;
            }
        }
    }

    public void StopAutoDrainBattery()
    {
        if (m_AutoDrainBatteryRoutine == null) { return; }
        
        StopCoroutine(m_AutoDrainBatteryRoutine);
        m_AutoDrainBatteryRoutine = null;
    }

    private IEnumerator AutoDrainBattery()
    {
        var delay = new WaitForSeconds(DRAIN_INTERVAL);
        
        while (m_BatteryLevel > 0.0f)
        {
            ChangeBatteryCharge(DRAIN_RATE);
            yield return delay;
        }
        
        OnBatteryChargeChanged?.Invoke(0.0f);
        yield return new WaitForEndOfFrame();
        TriggerBatteryIsOutOfCharge();
        
        yield return null;
    }
    
    private void TriggerBatteryIsOutOfCharge()
    {
        StopAutoDrainBattery();
        OnBatteryIsFlat?.Invoke();
    }
}
