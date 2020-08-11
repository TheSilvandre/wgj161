using UnityEngine;

public class Pedestal : Interactable {

    //[Header("Variables")]

    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Item slottableItem;
    
    //[Header("Stats")]
    
    private bool slotted;
    
    
    protected override void OnHover() {
        Item currentItem = inventory.GetCurrentItem();

        if (currentItem == slottableItem && !slotted) {
            hoverText.enabled = true;
            hoverText.SetText("Insert");
        } else {
            hoverText.enabled = false;
        }
    }

    protected override void OnInteract() {
        if (inventory.GetCurrentItem() == slottableItem) {
            slotted = true;
                
            // Handle insert logic
            // Remove item from inventory
            inventory.RemoveCurrentItem(1);
            hoverText.enabled = false;
        }
    }
}
