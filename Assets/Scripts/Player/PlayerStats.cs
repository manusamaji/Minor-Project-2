using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float maxHealth;
     float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamagePlayer(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (player.stateMachine.currentState != PlayerStates.State.KnockBack) 
              Debug.Log("Player is Dead");
        }
    }
    public float GetCurrentHealth() 
    {
        return currentHealth;
    }
}
