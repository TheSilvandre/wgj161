using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] private List<Image> slotsUI = new List<Image>();
    
    

    private List<InventorySlot> inventorySlots;
    private bool inventoryFull = false;
    private int selectedSlot;


    private void Start() {
        inventorySlots = new List<InventorySlot>(slotsUI.Count);
        for(int i = 0; i < inventorySlots.Capacity; i++) {
            inventorySlots.Add(new InventorySlot(0, null));
        }
        slotsUI[selectedSlot].color = Color.grey;
    }

    private void Update() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            slotsUI[selectedSlot].color = Color.white;
            selectedSlot++;
            if(selectedSlot >= inventorySlots.Count) {
                selectedSlot = 0;
            }
            slotsUI[selectedSlot].color = Color.grey;
            
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            slotsUI[selectedSlot].color = Color.white;
            selectedSlot--;
            if (selectedSlot < 0) {
                selectedSlot = inventorySlots.Count - 1;
            }

            slotsUI[selectedSlot].color = Color.grey;
            
        }
    }
    
    public bool AddItem(Item item) {
        Debug.Log("Trying to add item");
        for (int i = 0; i < inventorySlots.Count; i++) {
            InventorySlot slot = inventorySlots[i];
            if (slot.Amount != 0 && slot.Item == item) {
                slot.Amount += 1;
                slotsUI[i].sprite = item.icon;
                slotsUI[i].enabled = true;
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
                slotsUI[i].enabled = true;
                Debug.Log("Added item");
                return true;
            }
        }

        return false;
    }

    public void RemoveCurrentItem(int amount) {

        InventorySlot slot = inventorySlots[selectedSlot];

        if(slot.Amount >= amount) {
            slot.Amount -= amount;
            if (slot.Amount <= 0) {
                slot.Item = null;
                slot.Amount = 0;
                slotsUI[selectedSlot].sprite = null;
                slotsUI[selectedSlot].enabled = false;
            }
        }
    }
    
//    public bool UseCurrentItem(Transform clickedObject) {
//        if (inventorySlots[selectedSlot].Amount != 0) {
//            bool used = inventorySlots[selectedSlot].Item.Use(clickedObject);
//
//            if (used) {
//                inventorySlots[selectedSlot].Amount--;
//                if (inventorySlots[selectedSlot].Amount == 0) {
//                    inventorySlots[selectedSlot].Item = null;
//                    slotsUI[selectedSlot].sprite = null;
//                    slotsUI[selectedSlot].enabled = false;
//                }
//            }
//            
//            return used;
//        }
//
//        return false;
//    }

    public Item GetCurrentItem() {
        return inventorySlots[selectedSlot].Item;
    }
    
}