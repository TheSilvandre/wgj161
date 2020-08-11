using UnityEngine;

public class CrystalPickup : Interactable {
    
    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Item item;
    
    //[Header("Stats")]

    protected override void OnHover() {
        hoverText.enabled = true;
        hoverText.SetText("Pickup");
    }

    protected override void OnInteract() {
        bool itemAdded = playerInventory.AddItem(item);
        if (itemAdded) {
            Destroy(hoverText.gameObject);
            Destroy(gameObject);
        }
    }
}
