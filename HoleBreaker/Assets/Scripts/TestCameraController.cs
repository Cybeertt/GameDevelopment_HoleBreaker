using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCameraController : MonoBehaviour
{
    public float mouseSensitivity = 400f;
    public Transform playerBody;
    private float xRotation = 0f;
    public float fov = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.fieldOfView = fov;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

}