using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject placedBlocks;
    public GameObject filledBlocks;
    public GameObject filledBlockPrefab;

    public GameObject wallPrefab;    
    public BlockSystem bSys;
    private List<Vector3> unfilledBlocks;

    public GameObject outerWall;

    public TextMeshProUGUI a1, a3;

    // Start is called before the first frame update
    void Start()
    {
        generateWall(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r")) {
            generateWall(10);
        }
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

    private void makeHoles(int iterations, int n) {
        int count = 0;
        int i = 0;
        while (i < iterations && count != n) {
            foreach (Transform t in filledBlocks.transform) {
                if (UnityEngine.Random.Range(1, 6) == 1) {
                    unfilledBlocks.Add(t.transform.position);
                    DestroyImmediate(t.gameObject);
                    count++;                    
                } 
                if (count == n) break;
            }
            iterations++;
        }
    }

    //auxiliary method to generate walls to then make them into prefabs
    private void drawWall(int n) {
        unfilledBlocks = new List<Vector3>();
        
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
    }

    private int correctBlocks() {
        int count = 0;
        foreach (Transform b1 in placedBlocks.transform) 
        {
            foreach(Vector3 b2 in unfilledBlocks)
            {
                if (Mathf.Round(b1.position.y) == Mathf.Round(b2.y) && Mathf.Round(b1.position.z) == Mathf.Round(b2.z))
                {
                    count++;
                    break;
                }                
            }
        }

        return count;
    }

    //17, 1, 3
    /**
    Generates a random 7x4 wall given n holes to fill
    Has a left sided bias
    */
    public void generateWall(int n) {
//        a3.text = "" + correctBlocks();

        unfilledBlocks = new List<Vector3>();
        GameObject wall = Instantiate(wallPrefab, filledBlocks.transform.position, filledBlocks.transform.rotation);
        Destroy(filledBlocks);
        clearPlayerWall();
        
        filledBlocks = wall;
        filledBlocks.transform.SetParent(outerWall.transform);

        //Delete n random blocks
        //Stops at 10000 tries just in case
        makeHoles(10000, n);   
//        a1.text = "" + unfilledBlocks.Count;
    }
}
