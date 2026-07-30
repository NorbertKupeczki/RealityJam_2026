using UnityEngine;

public class SmallPowerSource : Drainable
{
    public override uint DrainPower()
    {
        throw new System.NotImplementedException();
    }

    public override void MarkObject()
    {
        Debug.Log("Marking small power source");
    }

    public override void UnmarkObject()
    {
        Debug.Log("Unmarking small power source");
    }
}
