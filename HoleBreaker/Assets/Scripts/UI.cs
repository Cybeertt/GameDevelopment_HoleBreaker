using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI:MonoBehaviour
{

    public GameObject pausemenu;
    public GameObject mainmenu;
    public GameObject points;
    public bool activegame;
    public bool play;
    public bool pause;

    public GameObject victory;
    public GameObject instruct;
    public GameObject instruct2;

    public float FOV;
    public float mouseSensitivity;


    //public  TMP_Text scoreAmountText;

    public GameObject[] levels;

    public GameObject currentLevel;

    private int currentLevelId;

    // Start is called before the first frame update
    void Start()
    {
        //changeLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(play == true) {
            if(Input.GetKeyDown(KeyCode.P))
            {
                if(activegame)
                {
                    ResumeGame();
                    
                } 
                else 
                {
                    Cursor.lockState = CursorLockMode.None;
                    activegame = true;
                    pause = true;
                    points.SetActive(false);
                    pausemenu.SetActive(true);
                    Time.timeScale = 0f;   
                    Cursor.visible = true;
                }
            }  

            if(Input.GetKey(KeyCode.T)){
                restartCurrentLevel();
            }
        }
        
    }

    public void PlayGame() {
        play = true;
        mainmenu.SetActive(false);
        points.SetActive(true);
        activelock();
    }

    public void ExitGame() {
        play = false;
        activegame = false;
        pause = false;
        mainmenu.SetActive(true);
        points.SetActive(false);
    }

    public void ResumeGame()
    {
        activegame = false;
        points.SetActive(true);
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        pause = false;
        activelock();
    }


    public void changeLevel(int levelId)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        currentLevel = Instantiate(levels[levelId], transform.parent);
        currentLevelId = levelId;
    }


    public void restartCurrentLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevel = Instantiate(levels[currentLevelId], transform.parent);
        }
        
    }

    public void destroyCurrentLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }

    public void OptionsPauseMenu()
    {
        if(pause)
        {
            pausemenu.SetActive(true);
        } else {
            mainmenu.SetActive(true);
        }
    }

    public void StopSound() 
    {
        instruct.SetActive(false);
        instruct2.SetActive(false);
    }

    public void ShowVictory()
    {
        victory.SetActive(true);
        //NextLevel();
    }

    public void activelock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
