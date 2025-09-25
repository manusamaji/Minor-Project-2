using UnityEngine;

public class Player : MonoBehaviour
{
    public GatherInput gatherInput;
    public StateMachine stateMachine;
    public PhysicsControl physicsControl;
    public Animator anim;

    private BaseAbility[] playerAbilities;
    private void Awake()
    {
        stateMachine = new StateMachine();
        playerAbilities = GetComponents<BaseAbility>();
        stateMachine.arrayofAbilities = playerAbilities;


    }

    private void Update()
    {
        foreach(BaseAbility ability in playerAbilities)
        {
            if (ability.thisAbilityState == stateMachine.currentState)
            {
                ability.ProcessAbility();
            }
            ability.UpdateAnimator();

        }
        Debug.Log("Current state is: " +stateMachine.currentState); 
    }


    private void FixedUpdate()
    {
        foreach(BaseAbility  ability in playerAbilities)
        {
            if(ability.thisAbilityState == stateMachine.currentState)
            {
                ability.ProcessFixedAbility();
            }
        }
    }
}