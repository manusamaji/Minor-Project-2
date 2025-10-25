using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchAbility : BaseAbility
{
    public InputActionReference crouchActionRef;
    private string crouchParameterName = "Crouch";
    private int crouchParameterID;

    protected override void Initialization()
    {
        base.Initialization();
        crouchParameterID=Animator.StringToHash(crouchParameterName);
    }

    private void OnEnable()
    {
        crouchActionRef.action.performed += TrytoCrouch;
        crouchActionRef.action.performed += StopCrouch;
    }
    private void OnDisable()
    {
        crouchActionRef.action.performed -= TrytoCrouch;
        crouchActionRef.action.performed -= StopCrouch;
    }
    public override void EnterAbility()
    {
        linkedPhysics.CrouchColliders();
    }

    public override void ExitAbility()
    {
        linkedPhysics.StandColliders();
    }

    private void TrytoCrouch (InputAction.CallbackContext value)
    {
        if(!isPermitted)
            return;
        if(linkedPhysics.grounded==false || linkedStateMachine.currentState==PlayerStates.State.Dash || linkedStateMachine.currentState==PlayerStates.State.Ladders)
            return;
        linkedStateMachine.ChangeState(PlayerStates.State.Crouch);
    }

    private void StopCrouch(InputAction.CallbackContext value)
    {
        if (!isPermitted)
        return;

        if(linkedStateMachine.currentState!=PlayerStates.State.Crouch)
            return;
        if (linkedPhysics.horizontalInput==0)
            linkedStateMachine.ChangeState(PlayerStates.State.Idle);
        else 
            if (linkedInput.horizontalInput==0)
            linkedStateMachine.ChangeState(PlayerStates.State.Run);
    }
    public override void UpdateAnimator()
    {
        linkedAnimator.SetBool(crouchParameterID, linkedStateMachine.currentState == PlayerStates.State.Crouch);

    }
}
