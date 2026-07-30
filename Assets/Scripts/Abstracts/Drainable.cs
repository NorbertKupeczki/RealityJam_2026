using UnityEngine;
using UnityEngine.Assertions;

public abstract class Drainable : MonoBehaviour, IInteractable
{
    [Header("Draineble")]
    [SerializeField, Range(10,100)] protected uint m_DrainableAmount;
    [SerializeField] protected bool m_IsDrainable;
    
    public bool IsDrainable => m_IsDrainable;
    
    protected Collider m_Collider;
    
    protected virtual void Awake()
    {
        if (!TryGetComponent<Collider>(out m_Collider))
        {
            Debug.LogError($"Collectable {gameObject.name} is missing a Collider!");
        }
    }
    
    protected virtual void Start()
    {
        
    }

    public abstract uint DrainPower();

    public abstract void MarkObject();

    public abstract void UnmarkObject();

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Drain;
    
    public GameObject InteractableGameObject => gameObject;
}
