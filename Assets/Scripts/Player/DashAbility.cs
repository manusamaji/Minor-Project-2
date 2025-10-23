using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : BaseAbility
{
    public InputActionReference dashActionRef;
    [SerializeField] private float dashForce;
    [SerializeField] private float maxDashDuration;
    private float dashTimer;

    private string dashAnimParamterName = "Dash";
    private int dashParamterID;


    protected override void Initialization()
    {
        base.Initialization();
        dashParamterID = Animator.StringToHash(dashAnimParamterName);
    }

    private void OnEnable()
    {
        dashActionRef.action.performed += TryToDash;
    }
    private void OnDisable()
    {
        dashActionRef.action.performed -= TryToDash;
    }

    public override void ExitAbility()
    {
        linkedPhysics.EnableGravity();
        //optional
        linkedPhysics.ResetVelocity();
    }

    private void TryToDash(InputAction.CallbackContext value)
    {
        if(!isPermitted)
            return;
        //other condition
        if (linkedStateMachine.currentState == PlayerStates.State.Dash || linkedPhysics.wallDetected)
            return;

        linkedStateMachine.ChangeState(PlayerStates.State.Dash);
        linkedPhysics.DisableGravity();
        linkedPhysics.ResetVelocity();

        if(player.facingRight)
            linkedPhysics.rb.linearVelocityX=dashForce;
        else
            linkedPhysics.rb.linearVelocityX=-dashForce;
        dashTimer=maxDashDuration;
    }

    public override void ProcessAbility()
    {
      dashTimer-= Time.deltaTime;
        if (linkedPhysics.wallDetected)
            dashTimer = -1;
        if(dashTimer <= 0)
        {
            if(linkedPhysics.grounded)
                linkedStateMachine.ChangeState(PlayerStates.State.Idle);
            else
                linkedStateMachine.ChangeState(PlayerStates.State.Jump);
        }
    }

    public override void UpdateAnimator()
    {
        linkedAnimator.SetBool(dashParamterID, linkedStateMachine.currentState==PlayerStates.State.Dash);
    }

}
