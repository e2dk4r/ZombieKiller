using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class ConfigurableActionButton
{
    public Button actionButton;
    public PlayerAction action;
}

public enum PlayerAction
{
    None,
    Left,
    Right,
    Up,
    Down,
    Fire
}

public class Keybindings : MonoBehaviour
{
    

    Dictionary<PlayerAction, KeyCode> keys = new Dictionary<PlayerAction, KeyCode>();
    public ConfigurableActionButton upButton, leftButton, rightButton, downButton, fireButton;
    public Button saveButton, mainMenuButton;

    private PlayerAction currentAction;
    private GameObject currentKey;

    private void Awake()
    {
        upButton.actionButton.onClick.AddListener(() =>
        {
            currentAction = PlayerAction.Up;
            currentKey = upButton.actionButton.gameObject;
        });
        leftButton.actionButton.onClick.AddListener(() =>
        {
            currentAction = PlayerAction.Left;
            currentKey = leftButton.actionButton.gameObject;
        });
        rightButton.actionButton.onClick.AddListener(() =>
        {
            currentAction = PlayerAction.Right;
            currentKey = rightButton.actionButton.gameObject;
        });
        downButton.actionButton.onClick.AddListener(() =>
        {
            currentAction = PlayerAction.Down;
            currentKey = downButton.actionButton.gameObject;
        });
        fireButton.actionButton.onClick.AddListener(() =>
        {
            currentAction = PlayerAction.Fire;
            currentKey = fireButton.actionButton.gameObject;
        });
        saveButton.onClick.AddListener(() => { SaveKeys(); });
        mainMenuButton.onClick.AddListener(() => { MainMenuScene(); });
    }

    private void Start()
    {
        RetrieveKeys(PlayerAction.Up, KeyCode.W);
        RetrieveKeys(PlayerAction.Left, KeyCode.A);
        RetrieveKeys(PlayerAction.Right, KeyCode.D);
        RetrieveKeys(PlayerAction.Down, KeyCode.S);
        RetrieveKeys(PlayerAction.Fire, KeyCode.Space);

        DisplayBinding(PlayerAction.Up, upButton);
        DisplayBinding(PlayerAction.Left, leftButton);
        DisplayBinding(PlayerAction.Right, rightButton);
        DisplayBinding(PlayerAction.Down, downButton);
        DisplayBinding(PlayerAction.Fire, fireButton);
    }

    private void OnGUI()
    {
        if (currentAction != PlayerAction.None)
        {
            Event e = Event.current;

            if (e.isKey && e.keyCode != KeyCode.Escape)
            {
                keys[currentAction] = e.keyCode;
                currentKey.transform.Find("Text").GetComponent<Text>().text = e.keyCode.ToString();
                currentAction = PlayerAction.None;
            }
        }
    }

    private void SaveKeys()
    {
        foreach (var k in keys)
        {
            PlayerPrefs.SetInt(k.Key.ToString(), (int)k.Value);
        }
        PlayerPrefs.Save();
    }

    private void RetrieveKeys(PlayerAction action, KeyCode defaultKey)
    {
        keys[action] = PlayerPrefs.HasKey(action.ToString()) ? (KeyCode)PlayerPrefs.GetInt(action.ToString()) : defaultKey;
    }

    public static void InitializePlayerAction(PlayerAction action, KeyCode defaultKey)
    {
        if (PlayerPrefs.HasKey(action.ToString()))
            return;

        PlayerPrefs.SetInt(action.ToString(), (int)defaultKey);
    }

    private void DisplayBinding(PlayerAction action, ConfigurableActionButton button)
    {
        button.actionButton.transform.Find("Text").GetComponent<Text>().text = keys[action].ToString();
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
