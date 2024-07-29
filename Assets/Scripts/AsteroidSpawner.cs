using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private int amountPerSpawn = 1;
    [SerializeField] [Range(0f, 45f)] private float trajectoryVariance = 15f;

    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 7f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    private IAsteroidFactory asteroidFactory;

    private void Awake()
    {
        asteroidFactory = new AsteroidFactory();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPoint = new Vector3(randomX, randomY, 0f);

            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            IAsteroid asteroid = asteroidFactory.Create(spawnPoint, rotation, Random.Range(asteroidPrefab.MinSize, asteroidPrefab.MaxSize));
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}