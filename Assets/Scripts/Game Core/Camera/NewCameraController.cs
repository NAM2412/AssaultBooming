using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float heightChangeSpeed = 3f;

    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();
        HandleHeightInput();
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void HandleRotationInput()
    {
        if (Input.GetMouseButton(1)) // Right mouse button pressed
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotation = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
            transform.eulerAngles += rotation;
        }
    }

    void HandleHeightInput()
    {
        float heightChange = 0f;

        if (Input.GetKey(KeyCode.Q)) // Press Q to move camera down
        {
            heightChange = -heightChangeSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E)) // Press E to move camera up
        {
            heightChange = heightChangeSpeed * Time.deltaTime;
        }

        Vector3 newPosition = transform.position + new Vector3(0f, heightChange, 0f);
        transform.position = newPosition;
    }
}
