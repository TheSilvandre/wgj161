using UnityEngine;

public class FuelController : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private FloatStorage fuel;
    [SerializeField] private BoolStorage inVehicle;

    [Header("Stats")]
    [SerializeField] private float depleteRate;

    private void Update() {
        if (inVehicle.value) {
            DepleteFuel();
        }
    }

    private void DepleteFuel() {
        fuel.value -= depleteRate * Time.deltaTime;
    }
}
