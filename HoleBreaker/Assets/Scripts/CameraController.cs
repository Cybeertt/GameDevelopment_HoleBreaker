using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    //public float realMouse;
    public Transform playerBody;
    private float xRotation = 0f;
    Camera maincamera;
    public float FOV;
    private int FirstPlayInt;
    //public float realZoom;
    private static readonly string MousePref = "MousePref";
    private static readonly string FirstPlay = "FirstPlay";
	private static readonly string ZoomPref = "ZoomPref";
    public Slider zoomslider;
    public float zoomfloat;
    public Slider senseslider;
    public float mousefloat;

    public  TMP_Text MouseValue;
    public  TMP_Text FOVValue;


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        
        maincamera = GetComponent<Camera>(); 
        FOV = 90f;
        mouseSensitivity = 400f;

        FirstPlayInt = PlayerPrefs.GetInt(FirstPlay);
		
		if(FirstPlayInt == 0)
		{
			zoomfloat = 0.9f;
			zoomslider.value = zoomfloat;
            mousefloat = 4.0f;
			senseslider.value = mousefloat;
			PlayerPrefs.SetFloat(ZoomPref, zoomfloat);
            PlayerPrefs.SetFloat(MousePref, mousefloat);
			PlayerPrefs.SetInt(FirstPlay, -1);
		}
		else
		{
			zoomfloat = PlayerPrefs.GetFloat(ZoomPref, zoomslider.value);
			zoomslider.value = zoomfloat;
            mousefloat = PlayerPrefs.GetFloat(MousePref, senseslider.value);
			senseslider.value = mousefloat;
		}

    }

    void Update()
    {
        Camera.main.fieldOfView = FOV;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void FOVSlider(float zoom)
    {
        FOV = zoom * 100f;
    }

    public void textMouse()
    {
        MouseValue.text = string.Format("{0:F1}",mouseSensitivity/100f);
    }

    public void textFOV()
    {
        FOVValue.text = string.Format("{0:F1}",FOV);
    }

    public void ChangeMouseSensitivity(float sense){
        mouseSensitivity = sense * 1000f;
    }

}