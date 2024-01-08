using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 0.3f;
    private float boostedSpeed = 50.0f;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = Input.mousePosition;
    }

    void Update()
    {
        // WASD keys movement
        float translation = Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? boostedSpeed : speed);
        float strafe = Input.GetAxis("Horizontal") * (Input.GetKey(KeyCode.LeftShift) ? boostedSpeed : speed);
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        // Mouse rotation
        Vector3 delta = Input.mousePosition - lastPosition;
        transform.Rotate(Vector3.down, -delta.x * sensitivity, Space.World); // Inverted horizontal axis
        transform.Rotate(Vector3.right, -delta.y * sensitivity, Space.Self); // Inverted vertical axis
        lastPosition = Input.mousePosition;
    }
}
