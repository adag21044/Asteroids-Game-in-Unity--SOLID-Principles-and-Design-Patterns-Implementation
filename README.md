# Asteroids Game in Unity: SOLID Principles and Design Patterns Implementation

This repository contains an Asteroids game developed in Unity, demonstrating the application of SOLID principles and various design patterns to ensure clean, maintainable, and scalable code.

## Key Features

### SOLID Principles
- **Single Responsibility Principle**: Each class has a single responsibility, such as the `Asteroid`, `Bullet`, and `Player` classes.
- **Open/Closed Principle**: The system is open for extension but closed for modification, evident in the use of interfaces like `IAsteroid` and `IAsteroidFactory`.
- **Liskov Substitution Principle**: Objects of a superclass can be replaced with objects of a subclass without affecting the correctness of the program.
- **Interface Segregation Principle**: Specific interfaces (`IAsteroid`, `IAsteroidFactory`) are created to ensure that classes don't implement unnecessary methods.
- **Dependency Inversion Principle**: High-level modules do not depend on low-level modules but on abstractions, illustrated by the dependency on interfaces rather than concrete implementations.

### Design Patterns
- **Factory Pattern**: Implemented through the `AsteroidFactory` class to handle the creation of asteroid objects.
- **Singleton Pattern**: Utilized for managing single instances where necessary (e.g., game managers).
- **Observer Pattern**: Can be used for managing events and state changes in the game.
- **Strategy Pattern**: Possible integration for defining different movement behaviors for asteroids or bullets.

## Contents
- **Scripts**: Contains all the C# scripts used in the project.
- **Prefabs**: Prefabricated game objects for asteroids, bullets, and the player.
- **Scenes**: Unity scenes demonstrating the game setup and mechanics.


## Script Overview
### `Asteroid.cs`
- Manages asteroid behavior, including initialization, movement, and collision handling.
- Implements the `IAsteroid` interface.

### `AsteroidFactory.cs`
- Implements the `IAsteroidFactory` interface to create asteroid instances.

### `AsteroidSpawner.cs`
- Handles the spawning of asteroids at set intervals and locations.

### `Bullet.cs`
- Manages bullet behavior, including shooting and collision detection.

### `Player.cs`
- Manages player controls, shooting, and screen wrapping behavior.

### `IAsteroid.cs`
- Interface for asteroid functionality.

### `IAsteroidFactory.cs`
- Interface for the asteroid factory.

## Contributing
Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License
This project is licensed under the MIT License.

---

Enjoy exploring the implementation of SOLID principles and design patterns in this Unity-based Asteroids game!
