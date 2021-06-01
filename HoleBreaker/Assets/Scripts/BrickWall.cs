using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    private bool isActivated = false;
    private bool fullyPressed = false;
    private float timer = 0;
    private Transform pass;

    private ParticleSystem particleSystem;

    public UI ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UI").gameObject.GetComponent<UI>() ;
        pass = transform.Find("Build");

        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        Bricks bricks;
        if(other.CompareTag("Bricks") ||
            other.TryGetComponent<Bricks>(out bricks))
        {
            isActivated = true;
        }
    }
}
