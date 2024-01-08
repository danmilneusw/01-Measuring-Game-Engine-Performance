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
