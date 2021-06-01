using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    public GameObject pausemenu;
    public bool activename;

    public GameObject victory;
    public GameObject instruct;
    public GameObject instruct2;

    public int scoreAmount = 0;

    public  TMP_Text scoreAmountText;

    public GameObject[] levels;

    public GameObject currentLevel;

    private int currentLevelID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(activename == false) {
                pausemenu.SetActive(true);
            } else {
                pausemenu.SetActive(false);
            }
        }
    }

    public void changeLevel(int levelID)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        currentLevel = Instantiate(levels[levelID], transform.parent);
        restoreValues();
        currentLevelID = levelID;
    }

    private void restoreValues()
    {
        scoreAmount = 0;
        scoreAmountText.text = scoreAmount.ToString();
    }

    public void restartCurrentLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevel = Instantiate(levels[currentLevelID], transform.parent);
            restoreValues();
        }
        
    }

    public void destroyCurrentLevel()
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel);
            restoreValues();
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
