using UnityEngine;

public class Hunger : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private FloatStorage hunger;

    [Header("Stats")]
    [SerializeField] private float hungerPerSecond = 5f;

    private void Update() {
        hunger.value -= hungerPerSecond * Time.deltaTime;

        if (hunger.value <= 0) {
            hunger.value = 0;
        }
    }
}
