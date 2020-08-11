using TMPro;
using UnityEngine;

public class FuelController : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private FloatStorage fuel;
    [SerializeField] private BoolStorage inVehicle;

    [Header("References")]
    [SerializeField] private TMP_Text fuelCounter;

    [Header("Stats")]
    [SerializeField] private float depleteRate;

    
    private void Update() {
        if (inVehicle.value) {
            DepleteFuel();
        }
    }

    private void DepleteFuel() {
        fuel.value -= depleteRate * Time.deltaTime;
        fuelCounter.text = fuel.value.ToString("N0");
    }
    
    public void AddFuel(float fuelValue) {
        fuel.value += fuelValue;
        if (fuel.value > 100) fuel.value = 100;
        fuelCounter.text = fuel.value.ToString("N0");
    }
    
}
