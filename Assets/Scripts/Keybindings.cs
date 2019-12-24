using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybindings : MonoBehaviour
{
    Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Button upButton, leftButton, rightButton, downButton, fireButton, saveButton;

    private GameObject currentKey;

    private void Awake()
    {
        upButton.onClick.AddListener(() => { currentKey = upButton.gameObject; });
        leftButton.onClick.AddListener(() => { currentKey = leftButton.gameObject; });
        rightButton.onClick.AddListener(() => { currentKey = rightButton.gameObject; });
        downButton.onClick.AddListener(() => { currentKey = downButton.gameObject; });
        fireButton.onClick.AddListener(() => { currentKey = fireButton.gameObject; });
        saveButton.onClick.AddListener(() => { SaveKeys(); });
    }

    private void Start()
    {
        keys[upButton.name] = PlayerPrefs.HasKey(upButton.name) ? (KeyCode)PlayerPrefs.GetInt(upButton.name) : KeyCode.W;
        keys[leftButton.name] = PlayerPrefs.HasKey(leftButton.name) ? (KeyCode)PlayerPrefs.GetInt(leftButton.name) : KeyCode.A;
        keys[rightButton.name] = PlayerPrefs.HasKey(rightButton.name) ? (KeyCode)PlayerPrefs.GetInt(rightButton.name) : KeyCode.D;
        keys[downButton.name] = PlayerPrefs.HasKey(downButton.name) ? (KeyCode)PlayerPrefs.GetInt(downButton.name) : KeyCode.S;
        keys[fireButton.name] = PlayerPrefs.HasKey(fireButton.name) ? (KeyCode)PlayerPrefs.GetInt(fireButton.name) : KeyCode.Space;

        upButton.transform.Find("Text").GetComponent<Text>().text = keys[upButton.name].ToString();
        leftButton.transform.Find("Text").GetComponent<Text>().text = keys[leftButton.name].ToString();
        rightButton.transform.Find("Text").GetComponent<Text>().text = keys[rightButton.name].ToString();
        downButton.transform.Find("Text").GetComponent<Text>().text = keys[downButton.name].ToString();
        fireButton.transform.Find("Text").GetComponent<Text>().text = keys[fireButton.name].ToString();
    }

        private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;

            if (e.isKey && e.keyCode != KeyCode.Escape)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.Find("Text").GetComponent<Text>().text = e.keyCode.ToString();
                Debug.Log(currentKey.name + " => " + e.keyCode.ToString());
                currentKey = null;
            }
        }
    }

    private void SaveKeys()
    {
        foreach (var k in keys)
        {
            PlayerPrefs.SetInt(k.Key, (int)k.Value);
        }
        PlayerPrefs.Save();
    }
}
