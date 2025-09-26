using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAbility : BaseAbility
   
{
    public InputActionReference jumpActionRef;

    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeed;
    [SerializeField] private float minimumAirTime;
    private float startMinimumAirTime;

    protected override void Initialization()
    {
        base.Initialization();
        startMinimumAirTime = minimumAirTime;
    }
    private void OnEnable()
    {
        jumpActionRef.action.performed += TryToJump;
        jumpActionRef.action.canceled += StopJump;
    }
    private void OnDisable()
    {
        jumpActionRef.action.performed -= TryToJump;
        jumpActionRef.action.canceled -= StopJump;

        
    }
    public override void ProcessAbility()
    {
        player.Flip();
        minimumAirTime -= Time.deltaTime;
        if(linkedPhysics.grounded && minimumAirTime < 0)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Idle);
        }
    }
    public override void ProcessFixedAbility()
    {
        if (!linkedPhysics.grounded)
        {
            linkedPhysics.rb.linearVelocity = new Vector2(airSpeed * linkedInput.horizontalInput, linkedPhysics.rb.linearVelocityY);
        }
    }
    private void TryToJump(InputAction.CallbackContext Value)
    {
      if(!isPermitted==false)
            return;
      if (linkedPhysics.grounded)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Jump);
            linkedPhysics.rb.linearVelocity=new Vector2(airSpeed*linkedInput.horizontalInput,jumpForce);
            minimumAirTime = startMinimumAirTime;
        }
    }
    private void StopJump(InputAction.CallbackContext Value)
    {
        Debug.Log("STOPJUMP");
    }
}
