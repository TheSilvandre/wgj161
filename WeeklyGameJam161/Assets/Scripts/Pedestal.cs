using UnityEngine;

public class Pedestal : Interactable {

    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Item slottableItem;
    [SerializeField] private Pedestal nextPedestal;
    [SerializeField] private GameObject[] lightSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite socketedSprite;
    
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip rotateSound;

    [Header("Stats")]
    [SerializeField] private int correctSpriteNumber;
    [SerializeField] private int currentSpriteNumber;
    [SerializeField] private bool ready;
    
    private bool slotted;

    
    public bool IsReady() {
        return ready;
    }
    
    protected override void OnHover() {
        Item currentItem = inventory.GetCurrentItem();

        if (currentItem == slottableItem && !slotted) {
            hoverText.enabled = true;
            hoverText.SetText("Insert");
        } else if (slotted && ready) {
            hoverText.enabled = true;
            hoverText.SetText("Rotate");
        } else {
            hoverText.enabled = false;
        }
        
    }

    protected override void OnInteract() {
        if (inventory.GetCurrentItem() == slottableItem && !slotted) {
            slotted = true;
            spriteRenderer.sprite = socketedSprite;
            if (ready) {
                lightSprites[currentSpriteNumber].SetActive(true);
                hoverText.enabled = true;
                hoverText.SetText("Rotate");
            }
            // Remove item from inventory
            inventory.RemoveCurrentItem(1);
            // TODO update sprite
        } else if (slotted && ready) {
            // TODO Rotate mirror sprite
            sfxSource.PlayOneShot(rotateSound);
            lightSprites[currentSpriteNumber].SetActive(false);
            currentSpriteNumber++;
            if (currentSpriteNumber >= lightSprites.Length) currentSpriteNumber = 0;
            if (nextPedestal != null) {
                nextPedestal.SetReady(currentSpriteNumber == correctSpriteNumber);
            }
            lightSprites[currentSpriteNumber].SetActive(true);
        }
        
    }

    private void SetReady(bool value) {
        ready = value;
        if (slotted && ready) {
            lightSprites[currentSpriteNumber].SetActive(true);
        }
    }

    public bool IsOrientedCorrectly() {
        return currentSpriteNumber == correctSpriteNumber;
    }
}