using UnityEngine;
public interface IAsteroidFactory
{
    IAsteroid Create(Vector2 position, Quaternion rotation, float size);
}