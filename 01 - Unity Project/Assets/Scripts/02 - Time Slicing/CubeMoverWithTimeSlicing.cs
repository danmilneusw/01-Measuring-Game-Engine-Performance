using UnityEngine;

public class CubeMoverWithTimeSlicing : MonoBehaviour
{
    public Transform targetTransform; // Assign your target transform here in the inspector
    private int frameCount = 0; // Keep track of the number of frames

    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 direction = (targetTransform.position - transform.position).normalized; // Calculate the direction towards the target

            float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position); // Calculate the distance to the target

            // Only move the cube if it's more than 0.1 units away from the target
            if (distanceToTarget > 0.01f)
            {
                // If the cube is within a distance of 25 to the target object
                if (distanceToTarget <= 25)
                {
                    transform.position += direction * 0.01f;
                }
                // If the cube is within a distance of 75 to the target object
                else if (distanceToTarget <= 75 && frameCount % 50 == 0) // Only move every 50 frames
                {
                    transform.position += direction * 0.5f; // Move 50 times the distance
                }
                // If the cube is further than 75 units away from the target object
                else if (distanceToTarget > 75 && frameCount % 250 == 0) // Only move every 250 frames
                {
                    transform.position += direction * 2.5f; // Move 250 times the distance
                }
            }

            frameCount++; // Increment the frame count
        }
    }
}
