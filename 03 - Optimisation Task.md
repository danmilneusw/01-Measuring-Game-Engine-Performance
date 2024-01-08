# 03 - Projectile Optimisation Task
## Background
Open the scene called 03 - Projectile Optimisation. In this scene, there are many capsules that simulatenously fire a projectile towards a sphere when you press 'F'.

<div align="center">
  <a href="Images\03 - Optimisation Task\01 - Shoot Projectile Scene.png" target="_blank">
    <img src="Images\03 - Optimisation Task\01 - Shoot Projectile Scene.png" alt="Shoot Projectile Scene" style="height:300px;"/>
  </a>
</div>

Typically, it is quite inefficient for every projectile to constantly be checking if it is colliding with the surface and be destroyed. Unity actually comes with built-in collision detection methods that are actually pretty efficient at doing this. It's used in the code below...

```
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject spherePrefab; // Assign your Sphere prefab in the Inspector
    public float speed = 20f;
    public float offset = 1f; // Distance in front of the camera to instantiate the sphere

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Press 'F' to shoot the projectile
        {
            Vector3 spawnPosition = transform.position + transform.forward * offset;
            GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * speed;
            }

            // Add the Projectile script to the sphere at runtime
            sphere.AddComponent<Projectile>();
        }
    }
}

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
```

...But what if we're in a different game engine, perhaps one you have built yourself, and this feature isn't available. Open up the script called 'InefficientCollsionCheck'. It contains code similar to above, but doesn't include the OnCollisionEnter feature. Instead, it uses a custom collision checker that looks more like something you would develop for your own game engine, shown here:

```
using UnityEngine;

public class InefficientCollisionCheck : MonoBehaviour
{
    public GameObject spherePrefab; // Assign your Sphere prefab in the Inspector
    public LayerMask collisionLayer; // Assign the layer to check collisions against in the Inspector
    public float speed = 5f;
    public float offset = 1f; // Distance in front of the camera to instantiate the sphere

    void Update()
    {
        // Fire projectile
        if (Input.GetKeyDown(KeyCode.F)) // Press 'F' to shoot the projectile
        {
            Vector3 spawnPosition = transform.position + transform.forward * offset;
            GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * speed;
            }

            // Add the Projectile script to the sphere at runtime
            sphere.AddComponent<Projectile>();

            // Collision detection
            Collider[] colliders = Physics.OverlapSphere(sphere.transform.position, 1f, collisionLayer);
            if (colliders.Length > 0)
            {
                Destroy(sphere);
            }
        }
    }
}
```

You can highlight all the capsules in the scene view and select to use the more efficient ShootProjectile or the InefficientCollsion script, both shown above, to compare their performance.

<div align="center">
  <a href="Images\03 - Optimisation Task\02 - Scripts.png" target="_blank">
    <img src="Images\03 - Optimisation Task\02 - Scripts.png" alt="Scripts" style="height:50px;"/>
  </a>
</div>

## Task
Without using Unity’s built-in collision detection methods, code a new method for destroying the projectiles when they hit the sphere that is more efficient than the current 'InefficientCollsionCheck' script.

## Hint
These projectiles move at a constant rate and destroy when they reach an object. The target object being shot at does not move.
