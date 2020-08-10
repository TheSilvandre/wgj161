public class InventorySlot {
    public InventorySlot(int amount, Item item) {
        Amount = amount;
        Item = item;
    }

    public int Amount { get; set; }

    public Item Item { get; set; }
}
