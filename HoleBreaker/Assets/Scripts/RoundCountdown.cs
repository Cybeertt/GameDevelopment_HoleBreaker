using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCountdown : MonoBehaviour
{

    //private const int ROUND_TIME = 10;
    public float timeRemaining = 120; //A round of Hole Breaker is 2 minutes.
    public bool timerIsRunning = false;
    public TextMeshProUGUI countdownText;

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
                gameManager.GetComponent<Game>().stopGame();
                buildingManager.GetComponent<BuildingSystem>().turnOffBuilding();
                countdownText.gameObject.SetActive(false);
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

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void resetTimer()
    {
        timeRemaining = 120;
        timerIsRunning = true;
    }
}