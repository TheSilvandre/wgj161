using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Interactable : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float upShiftText = 0.8f;    // TODO find better name for this variable
    
    protected TMP_Text hoverText;
    private Camera cam;


    private void Awake() {
        cam = Camera.main;
    }

    private void InstantiateInteractionText() {
        if (hoverText == null) {
            GameObject interactText = Instantiate(textPrefab, canvas.transform);
            hoverText = interactText.GetComponent<TMP_Text>();
        }
    }

    protected abstract void OnHover();
    
    private void OnMouseEnter() {
        InstantiateInteractionText();
        OnHover();
    }

    private void OnMouseOver() {
        hoverText.rectTransform.position = cam.WorldToScreenPoint(transform.position + Vector3.up * upShiftText);
        
        if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse)) {
            OnInteract();
        }
    }

    protected abstract void OnInteract();

    private void OnMouseExit() {
        hoverText.enabled = false;
    }
    
}
