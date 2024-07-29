using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour, IAsteroid
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private IAsteroidFactory asteroidFactory;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float size = 1f;
    [SerializeField] private float minSize = 0.35f;
    [SerializeField] private float maxSize = 1.65f;
    [SerializeField] private float movementSpeed = 50f;
    [SerializeField] private float maxLifetime = 30f;

    public float Size => size;
    public float MinSize => minSize;
    public float MaxSize => maxSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        asteroidFactory = new AsteroidFactory();
    }

    private void Start()
    {
        InitializeAsteroid();
    }

    private void InitializeAsteroid()
    {
        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.value * 360f);
        transform.localScale = Vector3.one * size;
        rb.mass = size;
        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HandleCollisionWithBullet();
        }
    }

    private void HandleCollisionWithBullet()
    {
        if (size > minSize)
        {
            CreateSplit();
            CreateSplit();
        }
        DestroyAsteroid();
    }

    private void CreateSplit()
    {
        Vector2 position = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * 0.5f;
        Quaternion rotation = transform.rotation;
        IAsteroid newAsteroid = asteroidFactory.Create(position, rotation, size * 0.5f);
        newAsteroid.SetTrajectory(UnityEngine.Random.insideUnitCircle.normalized);
    }

    public void DestroyAsteroid()
    {
        Destroy(gameObject);
    }

    public void SetSize(float newSize)
    {
        size = newSize;
        transform.localScale = Vector3.one * size;
        rb.mass = size;
    }
}
