using UnityEngine;

public interface IInteractable
{
    public void MarkObject();
    public void UnmarkObject();
    
    public GameEnums.InteractionType InteractionType { get; }
    public GameObject InteractableGameObject { get; }
}
