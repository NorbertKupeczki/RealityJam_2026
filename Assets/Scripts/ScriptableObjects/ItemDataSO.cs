using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    [SerializeField] private string m_ItemName;
    [SerializeField] private GameEnums.CollectableTypes m_CollectableType;
    
    public string ItemName => m_ItemName;
    public GameEnums.CollectableTypes CollectableType => m_CollectableType;
}
