using System;
using UnityEngine;

public abstract class Usable : MonoBehaviour, IInteractable
{
    [SerializeField] protected GameObject m_SelectedObject;
    public event Action OnUse;

    protected virtual void Start()
    {
        m_SelectedObject.SetActive(false);
    }

    public void MarkObject()
    {
        m_SelectedObject.SetActive(true);
    }

    public void UnmarkObject()
    {        
        m_SelectedObject.SetActive(false);
    }

    public void Use()
    {
        OnUse?.Invoke();
    }

    public GameEnums.InteractionType InteractionType => GameEnums.InteractionType.Use;
    public GameObject InteractableGameObject => gameObject;
}
