using System.Collections;
using UnityEngine;

public class InefficientCubeRotation : MonoBehaviour
{
    // Private variable for rotation speed
    private float rotationSpeed = 10f;

    // Update method is called once per frame
    void Update()
    {
        // Call RotateCube method in each frame
        RotateCube();
        // Call PrintRotation method in each frame
        PrintRotation();
    }

    // Define RotateCube method
    void RotateCube()
    {
        // Run the loop 1000 times
        for (int i = 0; i < 1000; i++)
        {
            // Rotate the cube around the up axis at the speed of rotationSpeed per second
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    // Define GetUpAxisRotation method
    void PrintRotation()
    {
        // Get the rotation of the up axis of the cube
        Quaternion rotation = transform.rotation;
        // Print the rotation to the console
        Debug.Log("Up axis rotation: " + rotation.eulerAngles);
    }
}
