using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FuelPickup : MonoBehaviour, IInteractable {

    [Header("References")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Item item;
    [SerializeField] private GameObject interactTextPrefab;
    [SerializeField] private Canvas canvas;

    [Header("Stats")]
    [SerializeField] private float upShiftText = 0.8f;    // TODO find better name for this variable

    private bool canPickup;
    private Camera camera;
    private TMP_Text text;


    private void Start() {
        camera = Camera.main;
    }

    public void Interact() {
        bool itemAdded = playerInventory.AddItem(item);
        if (itemAdded) {
            Destroy(text.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter() {
        // Enable UI
        if (text == null) {
            GameObject interactText = Instantiate(interactTextPrefab, canvas.transform);
            text = interactText.GetComponent<TMP_Text>();
        }

        text.gameObject.SetActive(true);
    }

    private void OnMouseOver() {
        text.rectTransform.position = camera.WorldToScreenPoint(transform.position + Vector3.up * upShiftText);
        if (Input.GetMouseButton((int) MouseButton.LeftMouse) && canPickup) {
            Interact();
        }
    }

    private void OnMouseExit() {
        // Disable UI
        text.gameObject.SetActive(false);
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
