using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    public GameManager GM;
    public Button resume;
    public Button exit;
    public Button mainMenu;
	// Use this for initialization
	void Start () {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        resume.onClick.AddListener(GM.ResumeGame);
        exit.onClick.AddListener(GM.ExitGame);
        mainMenu.onClick.AddListener(GM.GoToMenu);
    }
}
