using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int wave = 8;
    public int enemyCount = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        Debug.Log(enemyCount);
        if (enemyCount == 0)
            NextWave();
    }

    void NextWave()
    {
        wave++;
        BoardManager.instance.SetupScene(wave);
    }
}
