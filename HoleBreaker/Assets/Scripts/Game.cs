using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject placedBlocks;
    public GameObject filledBlocks;
    public GameObject filledBlockPrefab;

    public GameObject wallPrefab;
    public GameObject filledEmptyBlockPrefab;
    
    public BlockSystem bSys;

    private Vector3[] unfilledBlocks;
    // Start is called before the first frame update
    void Start()
    {
        generateWall(6);
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
                    DestroyImmediate(t.gameObject);
                    count++;
                } 
                if (count == n) break;
            }
            iterations++;
        }
    }

    //auxiliary method to generate 
    private void drawWall(int n) {
        unfilledBlocks = new Vector3[n];
        
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

    //17, 1, 3
    /**
    Generates a random 7x4 wall given n holes to fill
    Has a left sided bias
    */
    public void generateWall(int n) {
        if (filledBlocks.transform.childCount > 0) 
        {
            Destroy(filledBlocks);
            clearPlayerWall();
        }
        
        GameObject wall = Instantiate(wallPrefab, wallPrefab.transform.position, Quaternion.identity);
        filledBlocks = wall;
        //Delete n random blocks
        //Stops at 10000 tries just in case
        makeHoles(10000, n);        
    }
}
