using UnityEngine;

public class IdleAbility : BaseAbility
{
    protected override void Initialization()
    {
        base.Initialization();
        // add more things
    }
    public override void ProcessAbility()
    {
        Debug.Log("This is the IDLE ability");
    }
}
