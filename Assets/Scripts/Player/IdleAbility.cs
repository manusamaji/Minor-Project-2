using UnityEngine;

public class IdleAbility : BaseAbility
{
    public string idleAnimParameterName = "Idle";
    private int idleParameterInt;
    protected override void Initialization()
    {
        base.Initialization();
        idleParameterInt = Animator.StringToHash(idleAnimParameterName);

        // add more things
    }
    public override void ProcessAbility()
    {
 
        if (linkedInput.horizontalInput != 0)
        {
          linkedStateMachine.ChangeState(PlayerStates.State.Run);                                      
        }
    }
    public override void UpdateAnimator()
    {
        linkedAnimator.SetBool(idleParameterInt, linkedStateMachine.currentState == PlayerStates.State.Idle);
    }
}
