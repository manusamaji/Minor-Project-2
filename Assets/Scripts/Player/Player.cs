using UnityEngine;

public class Player : MonoBehaviour
{
    public GatherInput gatherInput;
    public StateMachine StateMachine;

    private BaseAbility[] playerAbilities;
    private void Awake()
    {
        StateMachine = new StateMachine();
        playerAbilities = GetComponents<BaseAbility>();
        StateMachine.arrayofAbilities = playerAbilities;


    }

    private void Update()
    {
        foreach(BaseAbility ability in playerAbilities)
        {
            if (ability.thisAbilityState == StateMachine.currentState)
            {
                ability.ProcessAbility();
            }
        }
    }


    private void FixedUpdate()
    {
        foreach(BaseAbility  ability in playerAbilities)
        {
            if(ability.thisAbilityState == StateMachine.currentState)
            {
                ability.ProcessFixedAbility();
            }
        }
    }
}