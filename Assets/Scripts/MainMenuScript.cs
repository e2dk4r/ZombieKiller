using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void InfoMenu()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
