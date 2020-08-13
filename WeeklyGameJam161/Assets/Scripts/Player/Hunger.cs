using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hunger : Interactable {
    
    [Header("References")]
    [SerializeField] private FloatStorage hunger; 
    [SerializeField] private FloatStorage health; 
    [SerializeField] private TMP_Text hungerCounter;
    [SerializeField] private TMP_Text healthCounter;
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Stats")]
    [SerializeField] private float hungerPerSecond = 5f;
    [SerializeField] private float hungerDamageBuffer = 2f;

    private float timeAtLastDamage;
    

    private void Update() {
        hunger.value -= hungerPerSecond * Time.deltaTime;

        if (hunger.value <= 0) {
            hunger.value = 0;
            if (timeAtLastDamage + hungerDamageBuffer < Time.time) {
                health.value -= 1;
                
                if(health.value <= 0) {
                    SceneManager.LoadScene("Menu");
                }
                
                healthCounter.SetText(health.value.ToString("N0"));
                timeAtLastDamage = Time.time;
            }
        }
        
        hungerCounter.SetText(hunger.value.ToString("N0"));
    }

    protected override void OnHover() {
        if (playerInventory.GetCurrentItem() is FoodObject) {
            hoverText.enabled = true;
            hoverText.SetText("Eat");
        }
    }

    protected override void OnInteract() {
        if (playerInventory.GetCurrentItem() is FoodObject) {
            hunger.value += ((FoodObject) playerInventory.GetCurrentItem()).hungerValue;
            playerInventory.RemoveCurrentItem(1);
        }
    }
}
