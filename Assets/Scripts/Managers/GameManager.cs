using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Game manager - remembers player progress, actually controlls level switching, 
/// </summary>
public class GameManager : MonoBehaviour {

    public ScoreTextController scoreText;

    public GameObject pauseMenuPrefab;
    private GameObject pauseMenu=null;

    public GameObject loseScreenPrefab;
    public GameObject player;
    private playerController playerScript;

    public int currentValue = 0;
    public int lastHeight = 0;

    public bool dead = false;



    //udělat state místo několika booleů, takhle je to divný, a potřebuju checkovat aby se nedělo něco co nemá (např. pause když je hráč mrtvý)
    protected bool paused = false;

    float fixedDeltaTime;

    private void Start()
    {
        //set money to actual amount
        Debug.Log(player);
        playerScript = player.GetComponent<playerController>();
    }

    private void Update()
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
        AddMoneyForHeight();
    }

    void Awake () {
        fixedDeltaTime = DataManager.instance.fixedDeltaTime;
        ResumeGame();
    }

    private void AddMoneyForHeight()
    {
        
    }

    /// <summary>
    /// Toggles the pause menu
    /// </summary>
    public void PauseGame(bool withMenu)
    {
        paused = true;
        if (withMenu)
        {
            if (pauseMenu == null)
            {
                pauseMenu = Instantiate(pauseMenuPrefab);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }

        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

    }



    public void ResumeGame()
    {
        paused = false;
        if (pauseMenu!=null)
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = DataManager.instance.fixedDeltaTime;
    }


    /// <summary>
    /// Resets current level
    /// </summary>
    public void ResetLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    public void ExitGame()
    {
        Application.Quit();
    }


    public void GoToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(DataManager.instance.menuSceneIndex);
        paused = false;
    }


    public void CollectiblePicked(int value)
    {
        currentValue+=(int)(value*playerScript.GetPointMultiplier() * (DataManager.instance.selectedLevel+1));
        scoreText.SetScore(currentValue);
    }

    /// <summary>
    /// Called after losing level
    /// </summary>
    public virtual void LoseLevel()
    {
        PauseGame(false);
        dead = true;
        Instantiate(loseScreenPrefab);
    }

}
