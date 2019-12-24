using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindingController : MonoBehaviour
{
    void Start()
    {
        Keybindings.InitializePlayerAction(PlayerAction.Up, KeyCode.W);
        Keybindings.InitializePlayerAction(PlayerAction.Left, KeyCode.A);
        Keybindings.InitializePlayerAction(PlayerAction.Right, KeyCode.D);
        Keybindings.InitializePlayerAction(PlayerAction.Down, KeyCode.S);
        Keybindings.InitializePlayerAction(PlayerAction.Fire, KeyCode.Space);
    }
}
