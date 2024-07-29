using UnityEngine;

public class AsteroidFactory : IAsteroidFactory
{
    public IAsteroid Create(Vector2 position, Quaternion rotation, float size)
    {
        Asteroid asteroid = Object.Instantiate(Resources.Load<Asteroid>("Asteroid"), position, rotation);
        asteroid.SetSize(size);
        return asteroid;
    }
}