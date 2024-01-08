using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public Transform targetTransform; // Assign your target transform here in the inspector

    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 direction = (targetTransform.position - transform.position).normalized; // Calculate the direction towards the target

            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position); // Calculate the distance to the target

            // Only move the cube if it's more than 0.1 units away from the target
            if (distanceToTarget > 0.01f)
            {
                transform.position += direction * 0.01f;
            }
        }
    }
}