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