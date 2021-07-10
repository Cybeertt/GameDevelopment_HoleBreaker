using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensivity : MonoBehaviour
{
    public Slider slider;

    public CameraController cameracontroller;

    void Start()
    {
        cameracontroller.mouseSensitivity = 400f;
    }

    public void ChangeMouseSensitivity(){
        cameracontroller.mouseSensitivity = slider.value * 1000f;
    }
}
