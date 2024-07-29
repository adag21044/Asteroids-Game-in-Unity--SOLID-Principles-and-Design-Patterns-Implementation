using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float thrustSpeed = 1f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private float respawnInvulnerability = 3f;
    [SerializeField] private bool screenWrapping = true;

    private bool thrusting;
    private float turnDirection;
    private Bounds screenBounds;

    private int playerLayer;
    private int ignoreCollisionsLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        ignoreCollisionsLayer = LayerMask.NameToLayer("Ignore Collisions");

        if (playerLayer == -1 || ignoreCollisionsLayer == -1)
        {
            Debug.LogError("One or both of the layers are not defined. Make sure 'Player' and 'Ignore Collisions' layers exist.");
        }
    }

    private void Start()
    {
        InitializeScreenBounds();
    }

    private void OnEnable()
    {
        TurnOffCollisions();
        Invoke(nameof(TurnOnCollisions), respawnInvulnerability);
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustSpeed);
        }

        if (turnDirection != 0f)
        {
            rb.AddTorque(rotationSpeed * turnDirection);
        }

        if (screenWrapping)
        {
            ScreenWrap();
        }
    }

    private void HandleInput()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void ScreenWrap()
    {
        if (rb.position.x > screenBounds.max.x + 0.5f)
        {
            rb.position = new Vector2(screenBounds.min.x - 0.5f, rb.position.y);
        }
        else if (rb.position.x < screenBounds.min.x - 0.5f)
        {
            rb.position = new Vector2(screenBounds.max.x + 0.5f, rb.position.y);
        }
        else if (rb.position.y > screenBounds.max.y + 0.5f)
        {
            rb.position = new Vector2(rb.position.x, screenBounds.min.y - 0.5f);
        }
        else if (rb.position.y < screenBounds.min.y - 0.5f)
        {
            rb.position = new Vector2(rb.position.x, screenBounds.max.y + 0.5f);
        }
    }

    private void Shoot()
    {
        Vector3 offset = new Vector3(0.1f, 0.1f, 0f);
        Vector3 shootPosition = transform.position + offset;
        Bullet bullet = Instantiate(bulletPrefab, shootPosition, transform.rotation);
        bullet.Shoot(transform.up);
    }

    private void TurnOffCollisions()
    {
        if (ignoreCollisionsLayer != -1)
        {
            gameObject.layer = ignoreCollisionsLayer;
        }
    }

    private void TurnOnCollisions()
    {
        if (playerLayer != -1)
        {
            gameObject.layer = playerLayer;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void InitializeScreenBounds()
    {
        GameObject[] boundaries = GameObject.FindGameObjectsWithTag("Boundary");
        foreach (var boundary in boundaries)
        {
            boundary.SetActive(!screenWrapping);
        }
        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));
    }

    public float RespawnDelay => respawnDelay;
}
