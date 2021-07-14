using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    //public TextMeshProUGUI a1, a3;

    private float timePassed = 0f;
    public int countdownTime;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text build, destroy;

    private GameObject currentKey;

    private Color32 normal = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    KeyCode[] mouseKeys;
    void Awake()
    {
        mouseKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6 };
    }

    void Start()
    {
        keys.Add("Build", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Build", "Mouse0")));
        keys.Add("Destroy", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Destroy", "Mouse1")));

        build.text = keys["Build"].ToString();
        destroy.text = keys["Destroy"].ToString();

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

    public void turnOnBuilding() {
        buildModeOn = true;
        countdownTime = 5;
    }

    public void turnOffBuilding() {
        buildModeOn = false;
    }

    private void Update()
    {
        
        /*if (Input.GetKeyDown("e"))
        {
            buildModeOn = !buildModeOn;
        }*/

        /*while(countdownTime > 0) 
        {

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        yield return new  WaitForSeconds(0.1f);*/

        timePassed += Time.deltaTime;

        if (Input.GetKey(keys["Destroy"]) && timePassed >= 0.065f)
        {
            timePassed = 0f;
            RaycastHit destroyPos;
            if (Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out destroyPos, 5, buildableSurfacesLayer))
            {
                if (destroyPos.collider.gameObject.layer == 8)
                {
                    Destroy(destroyPos.collider.gameObject);
                }
            }
            //a1.text = "" + placedBlocks.transform.childCount;
        } else if (buildModeOn)
        {
            RaycastHit buildPosHit;

            if (Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out buildPosHit, 5, buildableSurfacesLayer))
            {
                Vector3 point = buildPosHit.point;
                buildPos = new Vector3(Mathf.Round(point.x), Mathf.Round(point.y), Mathf.Round(point.z));
                canBuild = true;

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

            if (Input.GetKey(keys["Build"]))
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
        //a1.text = "" + placedBlocks.transform.childCount;
    }
    /*
    private void DestroyBlock(Vector3 point) 
    {
        Collider[] hitColliders = Physics.OverlapSphere(buildPos, 1f);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("Destroy");
        }
    }*/


    void OnGUI()
    {
        
        if (currentKey != null)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            } 
            //Prevent duplicate keys
            if (e.isMouse)
            {
                for (int i = 0; i < mouseKeys.Length; ++i)
                {
                    if (Input.GetKeyDown(mouseKeys[i]))
                    {
                        keys[currentKey.name] = mouseKeys[i];
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = mouseKeys[i].ToString();
                        currentKey.GetComponent<Image>().color = normal;
                        currentKey = null;
                    }
                }
            } 
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            Debug.Log(key.Value);
        }

        PlayerPrefs.Save();
    }
}