using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{

    private const int PERFECT_WALL_BONUS = 1;
    //private int[] smallWallProgression = {3, 4, 4, 5, 5, 6, 6, 7, 8};
    private int[] smallWallProgression = {3, 4, 4, 5, 5, 6, 6, 7, 8, 8, 9, 9, 9, 10};
    private int[] bigWallProgression = {10, 11, 12, 14, 15, 16, 18, 20, 22};

    private int progression = 0;


    public GameObject placedBlocks;
    public GameObject filledBlocks;
    public GameObject filledBlockPrefab;

    public GameObject wallPrefab;    
    public BlockSystem bSys;
    private Dictionary<Vector3, bool> unfilledBlocks;

    public GameObject outerWall;

    public TextMeshProUGUI a1, a2, a3;

    private int totalScore = 0;

    private bool nextWallCooldown = false;

    private bool gameIsActive = false;
    

    // Start is called before the first frame update
    void Start()
    {
        unfilledBlocks = new Dictionary<Vector3, bool>();
        //generateWall(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsActive && Input.GetKeyDown("r") && nextWallCooldown == false) {
            

            if (progression >= smallWallProgression.Length - 1)
            {
                finishWall(smallWallProgression[smallWallProgression.Length - 1]);
                generateWall(smallWallProgression[smallWallProgression.Length - 1]);
            } else
            {
                finishWall(smallWallProgression[progression++]);
                generateWall(smallWallProgression[progression]);
            }
            
            Invoke("ResetCoolDown", 1.0f);
            nextWallCooldown = true;
        }
    }

    private void ResetCoolDown()
    {
        nextWallCooldown = false;
    }

    private void clearPlayerWall() {
        foreach (Transform child in placedBlocks.transform) {
                GameObject.Destroy(child.gameObject);
        }
    }

    //deprecated
    private void clearWall() {
        for(int i = filledBlocks.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = filledBlocks.transform.GetChild(i);
            GameObject.Destroy(child.gameObject);
            // do whatever you want
        }
    }

    private void PlaceBlock(Vector3 position)
    {
        GameObject newBlock = Instantiate(filledBlockPrefab, position, Quaternion.identity);
        newBlock.transform.parent = filledBlocks.transform;
        Block tempBlock = bSys.allBlocks[1];
        newBlock.name = tempBlock.blockName + "-Block";
        newBlock.GetComponent<MeshRenderer>().material = tempBlock.blockMaterial;
    }

    private void replaceOrAdd(Dictionary<Vector3, bool> unfilledBlocks, Vector3 key, bool value) {
        if (unfilledBlocks.ContainsKey(key)) {
            unfilledBlocks[key] = value;
        } else 
        {
            unfilledBlocks.Add(key, value);
        }
    }

    //generates n random holes with a limit of n iterations in order for there to not be an infinite loop.
    private void makeHoles(int iterations, int n) 
    {
        GameObject holes = new GameObject("holes");
        
        int count = 0;
        int i = 0;
        while (i < iterations && count != n) 
        {
            foreach (Transform t in filledBlocks.transform) 
            {
                if (!t.IsChildOf(holes.transform) && UnityEngine.Random.Range(1, 6) == 1)
                {
                    unfilledBlocks[t.position] = true;
                    t.SetParent(holes.transform);
                    count++;
                } else 
                {
                    if (!unfilledBlocks.ContainsKey(t.position)) 
                    {
                        unfilledBlocks[t.position] = false;
                    }
                }
                

                if (count == n) break;

            }
            i++;
        }
        Destroy(holes);
    }

    //auxiliary method to generate walls to then make them into prefabs
    /*    private void drawWall(int n) 
    {
        unfilledBlocks = new Dictionary<Vector3, Boolean>();
        
        Vector3 wallStartPosition = filledBlocks.transform.position;
        int x = (int) Mathf.Round(wallStartPosition.x);
        int y = (int) Mathf.Round(wallStartPosition.y);
        int z = (int) Mathf.Round(wallStartPosition.z);

        //Make a full wall
        for (int i = z; i > z - 7; i--) {
            for (int j = y; j <= y + 3; j++) {
                Vector3 p = new Vector3(x, j, i);
                PlaceBlock(p); 
            }    
        }
    }*/

    private int getMaxWallScore(Dictionary<Vector3, bool> unfilledBlocks)
    {
        int count = 0;
        foreach(KeyValuePair<Vector3, bool> entry in unfilledBlocks)
        {
            if (entry.Value == true) {
                count++;
            }
        }
        return (count - 1) + PERFECT_WALL_BONUS;
    }

    private int getWallScore(int n) 
    {
        int missingBlocks = 0;
        int misPlacedBlocks = 0;
        int correctBlocks = 0;
        bool foundBlock = false;
        //Comparing each required block with a player placed block 
        foreach(KeyValuePair<Vector3, bool> entry in unfilledBlocks)
        {
            Vector3 b2 = entry.Key;
            bool requiredBlock = entry.Value;

            foreach (Transform b1 in placedBlocks.transform) 
            {
                //Found block, this requires the walls to be aligned perfectly on the xy plane
                //In later versions this should be independent of coordinates and instead have empty lists with references of blocks added to each position
                if (Mathf.Round(b1.position.y) == Mathf.Round(b2.y) && (Mathf.Round(b1.position.z) == Mathf.Round(b2.z)))
                {
                    foundBlock = true;

                    //Misplaced block
                    if (requiredBlock == false)
                        misPlacedBlocks++;
                    else
                        correctBlocks++;

                    break;
                }
            }

            //Missing Block
            if (entry.Value == true && !foundBlock) 
            {
                missingBlocks++;
            }
            foundBlock = false;
        }

        //Debug.Log("correctBlocks" + correctBlocks + " - " + "missingBlocks" + missingBlocks + " - misPlacedBlocks" + misPlacedBlocks);

        int score = (n - 1) - missingBlocks - misPlacedBlocks;
        if (n == correctBlocks && missingBlocks == 0) 
            //n points extra for a perfect wall
            score += PERFECT_WALL_BONUS;
        if (score < 0)
            score = 0;

        return score;
    }

    //17, 1, 3
    /**
    Generates a random 7x4 wall given n holes to fill
    */
    public void generateWall(int n) 
    {
        GameObject wall = Instantiate(wallPrefab, filledBlocks.transform.position, filledBlocks.transform.rotation);
        Destroy(filledBlocks);
        
        filledBlocks = wall;
        filledBlocks.transform.SetParent(outerWall.transform);

        //Delete n random blocks
        //Stops at 10000 tries just in case
        makeHoles(30000, n);   
        a1.text = "" + getMaxWallScore(unfilledBlocks);
        
    }

    public void finishWall(int n)
    {
        clearPlayerWall();
        a2.text = "" + getWallScore(n);
        totalScore += getWallScore(n);
        a3.text = "" + totalScore;

        unfilledBlocks.Clear();
    }

    public void reset() 
    {
        progression = 0;
        a1.text = "0";
        a2.text = "0";
        a3.text = "0";

        clearPlayerWall();
        clearWall();
        unfilledBlocks.Clear();
        gameIsActive = false;
    }

    public void startGame()
    {
        gameIsActive = true;
        generateWall(smallWallProgression[0]);
    }

    public void stopGame()
    {
        gameIsActive = false;
        finishWall(progression);
    }
}
