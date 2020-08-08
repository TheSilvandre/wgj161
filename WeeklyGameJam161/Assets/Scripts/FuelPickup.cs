using UnityEngine;

public class FuelPickup : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private FloatStorage fuelAmount;
    
    [Header("Stats")]
    [SerializeField] private float replenishAmount = 10f;

    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Item item;

    private bool canPickup;

    private void Update() {
        if (canPickup && Input.GetKeyDown(KeyCode.E)) {
            bool itemAdded = playerInventory.AddItem(item);
            if (itemAdded) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickup = true;
            // TODO Show UI
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickup = false;
            // TODO Show UI
        }
    }
}
