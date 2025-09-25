using UnityEngine;

public class MoveAbility : BaseAbility
{
    [SerializeField] private float speed;
    protected override void Initialization()
    {
        base.Initialization();
    }
    public override void ProcessAbility()
    {
       
    }
    public override void ProcessFixedAbility()
    {
        linkedPhysics.rb.linearVelocity = new Vector2(speed * linkedInput.horizontalInput, linkedPhysics.rb.linearVelocityY);
    }
    public override void UpdateAnimator()
    {
       
    }
    
}
