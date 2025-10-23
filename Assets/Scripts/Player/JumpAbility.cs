using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAbility : BaseAbility
   
{
    public InputActionReference jumpActionRef;

    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeed;
    [SerializeField] private float minimumAirTime;
    private float startMinimumAirTime;

    private string jumpAnimParameterName = "Jump";
    private string ySpeedAnimParameterName = "ySpeed";
    private int jumpParameterID;
    private int ySpeedParameterID;


    protected override void Initialization()
    {
        base.Initialization();
        startMinimumAirTime = minimumAirTime;
        jumpParameterID = Animator.StringToHash(jumpAnimParameterName);
        ySpeedParameterID=Animator.StringToHash(ySpeedAnimParameterName);
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
        if(!linkedPhysics.grounded && linkedPhysics.wallDetected)
        {
            if (linkedPhysics.rb.linearVelocityY < 0)
            {
                linkedStateMachine.currentState = (PlayerStates.State.WallSlide);
            }
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
      if(isPermitted==false)
            return;
      if (linkedPhysics.grounded)
        {
            linkedStateMachine.ChangeState(PlayerStates.State.Jump);
            linkedPhysics.rb.AddForce(new Vector2(airSpeed * linkedInput.horizontalInput, jumpForce), ForceMode2D.Impulse);
            linkedPhysics.rb.linearVelocity=new Vector2(airSpeed*linkedInput.horizontalInput,jumpForce);
            minimumAirTime = startMinimumAirTime;
            Debug.Log("JUMP Bwworkin um");
        }
    }
    private void StopJump(InputAction.CallbackContext Value)
    {
        Debug.Log("STOPJUMP");
    }
    public override void UpdateAnimator()
    {
       linkedAnimator.SetBool(jumpParameterID, linkedStateMachine.currentState ==  PlayerStates.State.Jump );
       linkedAnimator.SetFloat(ySpeedParameterID, linkedPhysics.rb.linearVelocityY);
    }
}
