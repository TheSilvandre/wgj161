public class InventorySlot {

    private int amount;
    private Item item;

    public InventorySlot(int amount, Item item) {
        this.amount = amount;
        this.item = item;
    }

    public int Amount {
        get => amount;
        set => amount = value;
    }

    public Item Item {
        get => item;
        set => item = value;
    }
}
