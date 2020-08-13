using UnityEngine;

public class FoodSource : Interactable {
    
    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Item item;
    [SerializeField] private SpriteRenderer currentSprite;
    [SerializeField] private Sprite foodSourceFull;
    [SerializeField] private Sprite foodSourceEmpty;

    [Header("Stats")]
    [SerializeField] private float foodRespawnTime;

    private bool foodAvailable = true;
    private bool canPickup;
    
    private float timeWhenPicked;


    private void Update() {
        if (timeWhenPicked + foodRespawnTime < Time.time) {
            RespawnFood();
        }
    }

    private void RespawnFood() {
        foodAvailable = true;
        currentSprite.sprite = foodSourceFull;
    }

    protected override void OnHover() {
        if (foodAvailable) {
            hoverText.enabled = true;
            hoverText.SetText("Harvest");
        }
    }

    protected override void OnInteract() {
        bool itemAdded = playerInventory.AddItem(item);
        if (itemAdded) {
            foodAvailable = false;
            currentSprite.sprite = foodSourceEmpty;
            timeWhenPicked = Time.time;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickup = false;
        }
    }
}