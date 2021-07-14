using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitialCountdown : MonoBehaviour
{

    //private const int ROUND_TIME = 10;
    public float timeRemaining = 3;
    public bool timerIsRunning = false;
    public TextMeshProUGUI countdownText;

    public GameObject roundCountdown;

    public GameObject gameManager;

    public GameObject buildingManager;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            } else
            {
                gameManager.GetComponent<Game>().startGame();
                buildingManager.GetComponent<BuildingSystem>().turnOnBuilding();
                countdownText.gameObject.SetActive(false);
                roundCountdown.SetActive(true);
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = "" + seconds;
    }

    public void resetTimer()
    {
        timeRemaining = 3;
        timerIsRunning = true;
    }
}