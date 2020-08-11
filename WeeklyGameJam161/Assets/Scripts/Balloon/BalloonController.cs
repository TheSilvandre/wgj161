using Cinemachine;
using UnityEngine;

public class BalloonController : Interactable {
    
    [Header("Variables")]
    [SerializeField] private bool canEnter;
    [SerializeField] private BoolStorage inVehicle;
    
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private FuelController fuelController;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float camSizeInVehicle = 15;
    [SerializeField] private float camSizeOnFoot = 7;
    [SerializeField] private float camSmoothValue = 2;

    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;


    private void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    protected override void OnHover() {
        hoverText.enabled = true;
        if (inventory.GetCurrentItem() is FuelObject) {
            hoverText.SetText("Add Fuel");
        } else if (!inVehicle.value) {
            hoverText.SetText("Enter");
        }
        else {
            hoverText.SetText("");
            hoverText.enabled = false;
        }
        
    }

    protected override void OnInteract() {
        Item item = inventory.GetCurrentItem();
        if (item is FuelObject) {
            inventory.RemoveCurrentItem(1);
            fuelController.AddFuel(1);
        }
        if (canEnter) {
            inVehicle.value = !inVehicle.value;
            player.parent = inVehicle.value ? transform : null;
        }
    }

    private void Update() {
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, inVehicle.value ? camSizeInVehicle : camSizeOnFoot, camSmoothValue * Time.deltaTime);

        // Movement
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
        if (horizontal != 0 && vertical != 0) {
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
