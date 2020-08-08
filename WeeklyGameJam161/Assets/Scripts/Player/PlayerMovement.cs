using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;

    [SerializeField] private BoolStorage inVehicle;
    public float runSpeed = 20.0f;

    private void Start () {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    private void FixedUpdate() {
        if (!inVehicle.value) {
            body.isKinematic = false;
            Move();
        }
        else {
            body.isKinematic = true;
        }
    }

    private void Move() {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
