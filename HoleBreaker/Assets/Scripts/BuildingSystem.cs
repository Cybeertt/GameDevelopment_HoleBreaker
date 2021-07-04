using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    [SerializeField]
    private Camera playerCamera;

    private bool buildModeOn = false;
    private bool canBuild = false;

    private BlockSystem bSys;

    [SerializeField]
    private LayerMask buildableSurfacesLayer;

    private Vector3 buildPos;

    private GameObject currentTemplateBlock;

    [SerializeField]
    private GameObject blockTemplatePrefab;
    [SerializeField]
    private GameObject blockPrefab;

    [SerializeField]
    private Material templateMaterial;

    public GameObject placedBlocks;
    public Transform start, end;
    private Vector3 startPosition, endPosition;

    private void Start()
    {
        bSys = GetComponent<BlockSystem>();
        currentTemplateBlock = Instantiate(blockTemplatePrefab, new Vector3(-10, -1000, -10), Quaternion.identity);
        startPosition = start.position;
        endPosition = end.position;
    }

    private bool isOutsideOfArea(Vector3 lookPos) {
        if (lookPos.x <= startPosition.x || lookPos.y <= startPosition.y || lookPos.z >= startPosition.z
        || lookPos.x >= endPosition.x || lookPos.y >= endPosition.y || lookPos.z <= endPosition.z)
            return true;

        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            buildModeOn = !buildModeOn;
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit destroyPos;
            if (Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out destroyPos, 10, buildableSurfacesLayer))
            {
                if (destroyPos.collider.gameObject.layer == 8)
                {
                    Destroy(destroyPos.collider.gameObject);
                }
            }
        } else if (buildModeOn)
        {
            RaycastHit buildPosHit;

            if (Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out buildPosHit, 10, buildableSurfacesLayer))
            {
                Vector3 point = buildPosHit.point;
                buildPos = new Vector3(Mathf.Round(point.x), Mathf.Round(point.y), Mathf.Round(point.z));
                canBuild = true;

                //Isto é mesmo a melhor maneira de fazer isto??? Parece um pouco à bruta
                if (isOutsideOfArea(point)) {
                    Destroy(currentTemplateBlock.gameObject);
                    canBuild = false;
                }
            }
            else
            {
                Destroy(currentTemplateBlock.gameObject);
                canBuild = false;
            }
        }

        if (!buildModeOn && currentTemplateBlock != null)
        {
            Destroy(currentTemplateBlock.gameObject);
            canBuild = false;
        }

        if (canBuild && currentTemplateBlock == null)
        {
            currentTemplateBlock = Instantiate(blockTemplatePrefab, buildPos, Quaternion.identity);
            currentTemplateBlock.GetComponent<MeshRenderer>().material = templateMaterial;
        }

        if (canBuild && currentTemplateBlock != null)
        {
            currentTemplateBlock.transform.position = buildPos;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBlock();
            }
        }
    }

    private void PlaceBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab, buildPos, Quaternion.identity);
        newBlock.transform.parent = placedBlocks.transform;
        Block tempBlock = bSys.allBlocks[0];
        newBlock.name = tempBlock.blockName + "-Block";
        newBlock.GetComponent<MeshRenderer>().material = tempBlock.blockMaterial;
    }
    
    private void DestroyBlock(Vector3 point) 
    {
        Collider[] hitColliders = Physics.OverlapSphere(buildPos, 1f);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("Destroy");
        }
    }
}