using UnityEngine;

public interface IAsteroid
{
    void SetTrajectory(Vector2 direction);
    float Size { get; }
    void DestroyAsteroid();
    void SetSize(float size);
}