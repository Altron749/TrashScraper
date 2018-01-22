using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinScreenController : MonoBehaviour {
    public GameManagerLevels GM;
    public Button nextLevel;
    public Button exit;
    public Button mainMenu;


    // Use this for initialization
    void Start () {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManagerLevels>();
        exit.onClick.AddListener(GM.ExitGame);
        mainMenu.onClick.AddListener(GM.GoToMenu);
    }


}
