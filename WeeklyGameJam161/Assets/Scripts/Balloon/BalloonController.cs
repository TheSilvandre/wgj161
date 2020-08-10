using UnityEngine;

public class BalloonController : MonoBehaviour {
    
    [Header("Variables")]
    [SerializeField] 

    private bool canEnter;

    [SerializeField] private Transform player;
    [SerializeField] private BoolStorage inVehicle;
    [SerializeField] private float moveSpeed = 10f;

    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;


    private void Start() {
        body = GetComponent<Rigidbody2D>();
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && canEnter) {
            inVehicle.value = !inVehicle.value;
            player.parent = inVehicle.value ? transform : null;
        }

        if (inVehicle.value) {
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
            player.position = transform.position;
        }
    }

    private void FixedUpdate() {
        if (inVehicle.value) {
            Move();
        } else {
            body.velocity = Vector2.zero;
        }
    }

    private void Move() {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 
        
        body.AddForce(new Vector2(horizontal * moveSpeed, vertical * moveSpeed));
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canEnter = false;
        }
    }
    
}
