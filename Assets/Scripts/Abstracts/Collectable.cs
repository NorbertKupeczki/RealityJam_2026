using UnityEngine;
using UnityEngine.Assertions;

public abstract class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemDataSO m_ItemData = null;
    
    public ItemDataSO ItemData => m_ItemData;

    protected Rigidbody m_Rigidbody;
    protected Collider m_Collider;
    
    protected virtual void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out m_Rigidbody))
        {
            Debug.LogError($"Collectable {gameObject.name} is missing a rigidbody!");
        }

        if (!TryGetComponent<Collider>(out m_Collider))
        {
            Debug.LogError($"Collectable {gameObject.name} is missing a Collider!");
        }
    }
    
    protected virtual void Start()
    {
        Assert.IsNotNull(m_ItemData, $"ItemData on {name} is null!");
        Assert.IsFalse(ItemData.CollectableType is GameEnums.CollectableTypes.Undefined, $"Collectable type of {name} is undefined!");
    }

    public virtual void Collect(Transform parent)
    {
        m_Rigidbody.useGravity = false;
        m_Rigidbody.isKinematic = true;
        m_Collider.enabled = false;
        
        transform.parent = parent;
        transform.position = parent.position;
        transform.rotation = parent.rotation;
    }

    public virtual void Drop(Vector3 dropPosition)
    {
        m_Rigidbody.useGravity = true;
        m_Rigidbody.isKinematic = false;
        m_Collider.enabled = true;
        
        transform.parent = null;
        transform.position = dropPosition;
    }

    public abstract void Use();

    public abstract void MarkObject();

    public abstract void UnmarkObject();

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Pickup;
    
    public GameObject InteractableGameObject => gameObject;
}
