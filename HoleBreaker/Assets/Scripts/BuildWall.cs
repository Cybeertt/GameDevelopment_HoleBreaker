using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWall : MonoBehaviour
{
    /*private bool isActivated = false;
    private bool fullyPressed = false;
    private float timer = 0;

    private ParticleSystem particleSystem;

    public UI ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").gameObject.GetComponent<UI>() ;

        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isActivated && !fullyPressed){
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, 1);
            button.localPosition = Vector3.Lerp(originalPosition,
                                pressedPosition,
                                timer);
            
            if(button.localPosition == pressedPosition){
                fullyPressed = true;
                particleSystem.Play();
                ui.StopSound();
                ui.ShowVictory();
            }
        }
        if(fullyPressed){
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 10, Time.deltaTime * 2f);            
        }
    }

    void OnTriggerEnter(Collider other){
        Wall wall;
        if(other.CompareTag("bricks") ||
            other.TryGetComponent<Wall>(out wall))
        {
            isActivated = true;
        }
    }*/
}
