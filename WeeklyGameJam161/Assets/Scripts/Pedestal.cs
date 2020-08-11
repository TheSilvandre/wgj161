using UnityEngine;

public class Pedestal : Interactable {

    //[Header("Variables")]

    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Item slottableItem;
    [SerializeField] private Pedestal nextPedestal;

    [Header("Stats")]
    [SerializeField] private Vector2 nextPedestalDir;
    [SerializeField] private Vector2 currentPedestalDir;
    
    private bool ready;
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
        if (inventory.GetCurrentItem() == slottableItem && ! slotted) {
            slotted = true;
            ready = true;
            
            // Remove item from inventory
            inventory.RemoveCurrentItem(1);
            hoverText.enabled = false;
            // TODO update sprite
        } else if (slotted) {
            // TODO Rotate mirror sprite
            currentPedestalDir = new Vector2(-currentPedestalDir.y, currentPedestalDir.x);
            nextPedestal.SetReady(currentPedestalDir == nextPedestalDir);
        }
    }

    private void SetReady(bool value) {
        ready = value;
    }
}
