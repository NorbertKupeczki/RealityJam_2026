using UnityEngine;

[CreateAssetMenu(fileName = "NewDrainableData", menuName = "ScriptableObjects/DrainableDataSO")]
public class DrainableItemSO : ScriptableObject
{
    [SerializeField] private string m_ItemName;
    
    public string ItemName => m_ItemName;
}
