using UnityEngine;

[CreateAssetMenu(fileName = "Fuel", menuName = "Item/Fuel")]
public class FuelObject : Item {

    public float fuelValue = 10;
    
    public override bool Use(Transform clickedObject) {
        FuelController fuelController = clickedObject.GetComponent<FuelController>();
        if (fuelController) {
            fuelController.AddFuel(fuelValue);
            return true;
        }

        return false;
    }
    
}
