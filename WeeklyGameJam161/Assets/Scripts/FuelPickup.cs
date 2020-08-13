using UnityEngine;

public class FuelPickup : Interactable {

    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Item item;

    //[Header("Stats")]

    private bool canPickup;
    

    protected override void OnHover() {
        hoverText.enabled = true;
        hoverText.SetText("Pickup");
        
    }

    protected override void OnInteract() {
        if (canPickup) {
            bool itemAdded = playerInventory.AddItem(item);
            if (itemAdded) {
                Destroy(hoverText.gameObject);
                Destroy(gameObject);
            }
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