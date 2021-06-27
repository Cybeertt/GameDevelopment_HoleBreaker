using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfig : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text up, left, down, right, jump, build, destroy;

    private GameObject currentKey;

    private Color32 normal = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    KeyCode[] mouseKeys;
    void Awake()
    {
        mouseKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6 };
    }

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keys.Add("Build", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Build", "Mouse0")));
        keys.Add("Destroy", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Destroy", "Mouse1")));
        
        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        jump.text = keys["Jump"].ToString();
        build.text = keys["Build"].ToString();
        destroy.text = keys["Destroy"].ToString();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Up");
        }

        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }

        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }

        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }

        if (Input.GetKeyDown(keys["Jump"]))
        {
            Debug.Log("Space");
        }

        if (Input.GetKeyDown(keys["Build"]))
        {
            Debug.Log("Build");
        }

        if (Input.GetKeyDown(keys["Destroy"]))
        {
            Debug.Log("Destroy");
        }*/

    }

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
        }

        PlayerPrefs.Save();
    }
}
