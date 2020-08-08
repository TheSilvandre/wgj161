using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] private List<Image> slotsUI = new List<Image>();

    private List<InventorySlot> inventorySlots;
    private bool inventoryFull = false;


    private void Start() {
        inventorySlots = new List<InventorySlot>(slotsUI.Count);
        for(int i = 0; i < inventorySlots.Capacity; i++) {
            inventorySlots.Add(new InventorySlot(0, null));
        }
    }


    public bool AddItem(Item item) {
        Debug.Log("Trying to add item");
        for (int i = 0; i < inventorySlots.Count; i++) {
            InventorySlot slot = inventorySlots[i];
            if (slot.Amount != 0 && slot.Item == item) {
                slot.Amount += 1;
                slotsUI[i].sprite = item.icon;
                Debug.Log("Added item to existing stack");
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Count; i++) {
            InventorySlot slot = inventorySlots[i];
            if (slot.Amount == 0) {
                slot.Item = item;
                slot.Amount = 1;
                slotsUI[i].sprite = item.icon;
                Debug.Log("Added item");
                return true;
            }
        }

        return false;
    }
    
}