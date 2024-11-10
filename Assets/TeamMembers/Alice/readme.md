# Projectile Prefab - The Shipwreck Protocol

## Projectile Summary
This is a small asteroid projectile prefab that the enemy of our space game, **The Shipwreck Protocol**, shoots. This prefab is available to download for free, along with the rest of our game, in the Unity downloads shop. This prefab uses a free PNG image of an asteroid A-10-95 from a pack found online. See video of projectiles in action submitted with this readme file.

## Component Summary
This prefab has the following components:
- Sprite Renderer (image)
- RigidBody2D
- Circle Collider 2D
- Projectile (script)

## Inspector Summary
This prefab has the following settings on Inspector:
- **speed**: the speed at which the projectile flies
- **life_of_projectile**: how long the projectile appears on screen
- **damage**: how much damage the projectile does

### Default Inspector Settings
The following numbers are default for these public Inspector settings:
- **speed**: `5f` (default)
- **life_of_projectile**: `3f` (default)
- **damage**: `5f` (default)

## Behavior and Code Notes
This projectile prefab has the following structure and behavior:

### Class Variables
- **speed**: public variable that can be adjusted by inspector (see Inspector summary). Refers to speed of projectile.
- **damage**: Represents the amount of damage the projectile deals. Public variable present in Inspector.
- **life_of_projectile**: Defines how long the projectile will exist before being destroyed (3 seconds by default). Public variable that can be adjusted in Inspector
- **rb**: Rigidbody2D component. This manages the movement of the projectile as it is shot. Private variable.

### Methods

- **Start (private)**
  - `rb = GetComponent<Rigidbody2D>();`: Gets the Rigidbody2D component on the GameObject.
  - `Destroy(gameObject, life_of_projectile);`: destroys our projectile GameObject after the set time that is defined by life_of_projectile (3 seconds). To have the projectile stay longer on screen, adjust the time.

- **Initialize (public)**
  - **Parameters**: a Vector2 direction. This is to be the travel direction for the projectile
  - `rb.velocity = direction * speed;`: Sets velocity of the projectile as it moves in a specific direction.

- **OnTriggerEnter2D (private)**
  - `other.CompareTag("Player")`: Checks the projectile collided with an object tagged "Player".
  - `Destroy(gameObject);`: Destroys the projectile when it hits.
