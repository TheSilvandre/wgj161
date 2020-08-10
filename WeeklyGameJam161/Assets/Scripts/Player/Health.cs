using UnityEngine;

public class Health : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private FloatStorage health;
    [SerializeField] private FloatStorage hunger;

    private float damageCooldown = 0;
    
    private void Update() {
        if (hunger.value <= 0) {
            if (damageCooldown > 1) {
                DealDamage(1);
            } else {
                damageCooldown += Time.deltaTime;
            }
            
        }
    }


    public void DealDamage(float damage) {
        health.value -= damage;
        
        // Update UI

        if (health.value <= 0) {
            Debug.Log("Player is dead");
        }

        damageCooldown = 0;
    }
    
    
    
}
