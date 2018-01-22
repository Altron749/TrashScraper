using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoseScreenController : MonoBehaviour {

    public GameManager GM;
    public Button restart;
    public Button exit;
    public Button mainMenu;
    public GameObject HighScoreTextPrefab;

    // Use this for initialization
    void Start () {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        restart.onClick.AddListener(GM.ResetLevel);
        exit.onClick.AddListener(GM.ExitGame);
        mainMenu.onClick.AddListener(GM.GoToMenu);
    }

    public void showHighScore()
    {
        float change = HighScoreTextPrefab.GetComponent<RectTransform>().rect.height;
        float currentPositionY = HighScoreTextPrefab.GetComponent<RectTransform>().anchoredPosition.y;
        float currentPositionX = HighScoreTextPrefab.GetComponent<RectTransform>().anchoredPosition.x;
        for (int i = 0; i < DataManager.instance.highscores.Count; i++)
        {
            currentPositionY -= change;
            GameObject tmp = Instantiate(HighScoreTextPrefab, transform);
            tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPositionX,currentPositionY);
            tmp.GetComponent<Text>().text = (i+1) + ". " + DataManager.instance.highscores[i].playerName + " " + DataManager.instance.highscores[i].score;
        }
    }

    public void showReachedScore(int score)
    {
        GameObject tmp = Instantiate(HighScoreTextPrefab,transform);
        tmp.GetComponent<Text>().text = "Reached score:  " + score;
        
    }

}
