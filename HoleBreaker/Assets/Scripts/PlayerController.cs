using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float speedMultiplier = 2.0f;
    private float currentSpeed;

    void Update()
    {
        currentSpeed = speed;
        float x = Input.GetAxis("HorizontalX");
        float z = Input.GetAxis("HorizontalZ");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.up * y + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift)) {
            currentSpeed = currentSpeed * speedMultiplier;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);
    }
}