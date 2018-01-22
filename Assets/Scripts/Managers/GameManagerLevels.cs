using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevels : GameManager {

    public GameObject winScreenPrefab;

    //stored current level;
    int currentLevel = 1;



    /// <summary>
    /// Called after finishing level
    /// </summary>
    public void WinLevel()
    {
        PauseGame(false); 
        Debug.Log("Winning LEvel");
        if (currentLevel < DataManager.instance.numberOfLevels)
        {
            DataManager.instance.UnlockNextLevel();
            Instantiate(winScreenPrefab);
        }
        else
        {
            WinGame();
        }
    }


    private void WinGame()
    {
        Instantiate(winScreenPrefab);
    }
}
