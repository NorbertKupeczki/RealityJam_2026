using UnityEngine;

[CreateAssetMenu(fileName = "NewChargeableData", menuName = "ScriptableObjects/ChargeableDataSO")]
public class ChargeableItemSO : ScriptableObject
{
    [SerializeField] private string m_ItemName;
    
    public string ItemName => m_ItemName;
}
