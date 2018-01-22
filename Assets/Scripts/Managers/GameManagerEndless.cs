using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Game manager - remembers player progress, actually controlls level switching, 
/// </summary>
public class GameManagerEndless : GameManager {
    public int Shownheigth = 0;
    public Text heigthText;
    public int heigthMultiplier = 5;
    public float deathTimer = 0;
    bool startedDrowning = false;
    float deathAnimTime = 3.5f;

    public Text highJumpIndicatorPrefab;
    public Text highJumpIndicator;

    public WaterController water;
    private void Update()
    {
        if (startedDrowning)
        {
            deathTimer += Time.deltaTime;
        }
        if (deathTimer>deathAnimTime && startedDrowning)
        {
            startedDrowning = false;
            LoseLevel();
        }
        if (Camera.main.transform.position.y - Shownheigth>10)
        {
            Shownheigth += 10;
        }
        if (!dead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!paused)
                {
                    PauseGame(true);
                }
                else
                {
                    ResumeGame();
                }
            }
        }
        
    }

    public void StartDrowning()
    {
        startedDrowning = true;
        Camera.main.GetComponent<CameraController>().Dead();
        water.setStartingSpeed();
    }

    override public void LoseLevel()
    {
        dead = true;
        if (!paused)
        {

        int reachedScore = currentValue + heigthMultiplier * Shownheigth;
        DataManager.instance.MoneyCollected(reachedScore);
        if (DataManager.instance.isInHighscore(reachedScore))
        {
            DataManager.instance.setNewHighScore(reachedScore);
        }
        PauseGame(false);
        GameObject loseScreen = Instantiate(loseScreenPrefab);
        loseScreen.GetComponent<LoseScreenController>().showHighScore();
        loseScreen.GetComponent<LoseScreenController>().showReachedScore(reachedScore);

        }

    }

    
    


}
