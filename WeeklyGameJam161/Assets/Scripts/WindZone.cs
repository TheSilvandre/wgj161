using UnityEngine;

public class WindZone : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] private Vector2 windDirection;
    [SerializeField] private float windStrength;

    
    private void Start() {
        windDirection.Normalize();
    }

    private void OnTriggerStay2D(Collider2D other) {

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null) {
            rb.AddForce(windDirection * windStrength);
        }
        
    }
}
