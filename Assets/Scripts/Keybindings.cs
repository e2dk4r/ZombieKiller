﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class ConfigurableActionButton
{
    public Button actionButton;
    public Keybindings.PlayerAction action;
}

public class Keybindings : MonoBehaviour
{
    public enum PlayerAction
    {
        None,
        Left,
        Right,
        Up,
        Down,
        Fire
    }

    Dictionary<PlayerAction, KeyCode> keys = new Dictionary<PlayerAction, KeyCode>();
    public ConfigurableActionButton upButton, leftButton, rightButton, downButton, fireButton;
    public Button saveButton;

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
    }

    private void Start()
    {
        InitializeKey(PlayerAction.Up, KeyCode.W);
        InitializeKey(PlayerAction.Left, KeyCode.A);
        InitializeKey(PlayerAction.Right, KeyCode.D);
        InitializeKey(PlayerAction.Down, KeyCode.S);
        InitializeKey(PlayerAction.Fire, KeyCode.Space);

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
                Debug.Log(currentAction.ToString() + " => " + e.keyCode.ToString());

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

    private void InitializeKey(PlayerAction action, KeyCode defaultKey)
    {
        keys[action] = PlayerPrefs.HasKey(action.ToString()) ? (KeyCode)PlayerPrefs.GetInt(action.ToString()) : defaultKey;
    }

    private void DisplayBinding(PlayerAction action, ConfigurableActionButton button)
    {
        button.actionButton.transform.Find("Text").GetComponent<Text>().text = keys[action].ToString();
    }
}