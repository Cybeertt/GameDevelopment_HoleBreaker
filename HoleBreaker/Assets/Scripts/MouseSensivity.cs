using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSensivity : MonoBehaviour
{
    public GameObject slider;

    public CameraController cameracontroller;

    private void ChangeMouseSensitivity(float X){
        cameracontroller.mouseSensitivity = X;
    }
}
