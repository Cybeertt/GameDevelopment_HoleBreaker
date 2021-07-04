using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI2 : MonoBehaviour
{

    public GameObject pausemenu;
    public GameObject mainmenu;
    public GameObject points;
    public bool activegame;
    public bool pause;

    public GameObject[] levels;

    public GameObject currentLevel;

    private int currentLevelId;

    // Start is called before the first frame update
    void Start()
    {
        changeLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.P))
        {
            if(activegame)
            {
                ResumeGame();
            } 
            else 
            {
                activegame = true;
                pause = true;
                points.SetActive(false);
                pausemenu.SetActive(true);
                Time.timeScale = 0f;   
                Cursor.visible = true;
            }
        }  

        if(Input.GetKey(KeyCode.R)){
            restartCurrentLevel();
        }
    }

    public void ResumeGame()
    {
        activegame = false;
        pause = false;
        points.SetActive(true);
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
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

    public void keybindingPauseMenu()
    {
        if(pause)
        {
            pausemenu.SetActive(true);
        } else {

        }
    }

}
