using UnityEngine;

public static class GameEnums
{
    public enum InteractionType
    {
        None = 0,
        Pickup,
        Drain,
        Charge,
        Insert,
        Use
    }
    
    public enum CollectableTypes
    {
        Undefined = 0,
        Battery,
        Component
    }
}
