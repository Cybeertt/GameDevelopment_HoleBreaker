using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    //public float realMouse;
    public Transform playerBody;
    private float xRotation = 0f;
    Camera maincamera;
    public float zoomAMT;
    private int FirstPlayInt;
    //public float realZoom;
    private static readonly string MousePref = "MousePref";
    private static readonly string FirstPlay = "FirstPlay";
	private static readonly string ZoomPref = "ZoomPref";
    public Slider zoomslider;
    public float zoomfloat;
    public Slider senseslider;
    public float mousefloat;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        
        maincamera = GetComponent<Camera>(); 
        zoomAMT = 100f;
        mouseSensitivity = 100f;

        FirstPlayInt = PlayerPrefs.GetInt(FirstPlay);
		
		if(FirstPlayInt == 0)
		{
			zoomfloat = zoomAMT * 0.0001f;
			zoomslider.value = zoomfloat;
            mousefloat = mouseSensitivity * 0.0001f;
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
        maincamera.fieldOfView = zoomAMT;
        
        //realZoom = zoomAMT * 0.01f;
        //realMouse = mouseSensitivity * 0.001f;
        //PlayerPrefs.SetFloat(MousePref, realMouse);
        //PlayerPrefs.SetFloat(ZoomPref, realZoom);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void sliderZoom( float zoom)
    {
        zoomAMT = zoom * 100f;
    }

    /*public void OnGUI()
    {
        //Set up the maximum and minimum values the Slider can return (you can change these)
        float max, min;
        max = 150.0f;
        min = 20.0f;
        //This Slider changes the field of view of the Camera between the minimum and maximum values
        zoomAMT = GUI.HorizontalSlider(new Rect(20, 20, 100, 40), zoomAMT, min, max);
    }*/

    public void ChangeMouseSensitivity(float sense){
        mouseSensitivity = sense * 1000f;
    }

}