using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class FuelController : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private FloatStorage fuel;
    [SerializeField] private BoolStorage inVehicle;

    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerInventory inventory;

    [Header("Stats")]
    [SerializeField] private float depleteRate;

    private void Update() {
        if (inVehicle.value) {
            DepleteFuel();
        }
    }

    private void DepleteFuel() {
        fuel.value -= depleteRate * Time.deltaTime;
        slider.value = fuel.value / 100;
    }

    private void OnMouseOver() {
        if (Input.GetMouseButton((int) MouseButton.LeftMouse)) {
            inventory.UseCurrentItem(transform);
        }
    }

    public void AddFuel(float fuelValue) {
        fuel.value += fuelValue;
        if (fuel.value > 100) fuel.value = 100;
        slider.value = fuel.value / 100;
    }
}
