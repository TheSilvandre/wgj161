using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonController : Interactable {
    
    [Header("Variables")]
    [SerializeField] private bool canEnter;
    [SerializeField] private BoolStorage inVehicle;
    [SerializeField] private FloatStorage fuel;
    
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private FuelController fuelController;
    [SerializeField] private SpriteRenderer balloonSpriteRenderer;
    [SerializeField] private Sprite balloonWithoutPlayer;
    [SerializeField] private Sprite balloonWithPlayer;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip endGameMusic;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip fuelAddSound;
    [SerializeField] private GameObject fuelCounter;

    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float camSizeInVehicle = 15;
    [SerializeField] private float camSizeOnFoot = 7;
    [SerializeField] private float camSmoothValue = 2;

    private Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;

    private bool inCutscene;
    

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        fuelCounter.SetActive(true);
    }

    protected override void OnHover() {
        UpdateText();
    }

    private void UpdateText() {
        hoverText.enabled = true;
        if (inventory.GetCurrentItem() is FuelObject) {
            hoverText.SetText("Add Fuel " + fuel.value + "/" + "10");
        } else if(fuel.value < 10) {
            hoverText.SetText("Fuel " + fuel.value + "/" + "10");
        }else if (!inVehicle.value) {
            hoverText.SetText("Enter");
        } else {
            hoverText.SetText("");
            hoverText.enabled = false;
        }
    }

    protected override void OnInteract() {
        Item item = inventory.GetCurrentItem();
        if (item is FuelObject) {
            inventory.RemoveCurrentItem(1);
            fuelController.AddFuel(1);
            sfxSource.PlayOneShot(fuelAddSound);
        } else if (canEnter && !inCutscene && fuel.value >= 10) {
            inVehicle.value = !inVehicle.value;
            player.parent = inVehicle.value ? transform : null;
            balloonSpriteRenderer.sprite = inVehicle.value ? balloonWithPlayer : balloonWithoutPlayer;

            inCutscene = true;

            StartCoroutine(FadeOutMusic(5));
            StartCoroutine(CutsceneAnimation());
        }
        UpdateText();
    }

    private IEnumerator CutsceneAnimation() {
        yield return new WaitForSeconds(5);
        musicSource.PlayOneShot(endGameMusic);
        fadeAnimator.gameObject.SetActive(true);
        fadeAnimator.Play("Fade");
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
    }
    
    private IEnumerator FadeOutMusic (float fadeTime) {
        float startVolume = musicSource.volume;
 
        while (musicSource.volume > 0) {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }
 
        musicSource.Stop ();
        musicSource.volume = startVolume;
    }


    private void Update() {
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, inVehicle.value ? camSizeInVehicle : camSizeOnFoot, camSmoothValue * Time.deltaTime);

        if (inCutscene) {
            vertical = 1;
        }
        
        // Movement
        if (inVehicle.value) {
            //horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            //vertical = Input.GetAxisRaw("Vertical"); // -1 is down
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
