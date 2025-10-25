using UnityEngine;

public class MoveAbility : BaseAbility
{
    [SerializeField] private float speed;
    private string runAnimParameterName = "Run";
    private int runParameterID;
    protected override void Initialization()
    {
        base.Initialization();
        runParameterID = Animator.StringToHash(runAnimParameterName);
    }
    public override void ProcessAbility()
    {
        if (linkedInput.horizontalInput == 0)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Idle);
        }
    }
    public override void ProcessFixedAbility()
    {
        Vector2 movementVector = new Vector2(speed * linkedInput.horizontalInput, linkedPhysics.rb.linearVelocityY);
        linkedPhysics.rb.AddForce(movementVector);
        linkedPhysics.rb.linearVelocity = new Vector2(speed * linkedInput.horizontalInput, linkedPhysics.rb.linearVelocityY);
        Debug.Log($"Input = {linkedInput.horizontalInput} || Linear Velo Sent to Physics Object = {linkedPhysics.rb.linearVelocity}");
    }
    public override void UpdateAnimator()
    {
       linkedAnimator.SetBool(runParameterID,linkedStateMachine.currentState==PlayerStates.State.Run);
    }
    
}
