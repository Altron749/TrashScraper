using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    //private int selectedWorld = 0;

    //public GameObject buttonWorld1;
    //public GameObject buttonWorld2;

    public Button play;
    public Button help;
    public Button store;
    public Button exit;

    public Animator backgroundAnim;
    public Animator rainAnim;
    public Animator cloudAnim;

    public Animator buttonPlay;
    public Animator buttonHelp;
    public Animator buttonStore;
    public Animator buttonExit;


    public RainController rainSounds;

    // Use this for initialization
    void Start () {
        play.onClick.AddListener(startClicked);
        exit.onClick.AddListener(ExitGame);
        help.onClick.AddListener(goHelp);
        store.onClick.AddListener(storeClicked);
        //buttonWorld1.GetComponent<Button>().onClick.AddListener(world1Selected);
        //buttonWorld2.GetComponent<Button>().onClick.AddListener(world2Selected);
        
        //show selected world
        /*if (DataManager.instance.selectedLevel == 0)
        {
            buttonWorld1.GetComponent<WorldButtonController>().Selected();
            buttonWorld2.GetComponent<WorldButtonController>().Deselected();
        } else if (DataManager.instance.selectedLevel == 1)
        {
            buttonWorld2.GetComponent<WorldButtonController>().Selected();
            buttonWorld1.GetComponent<WorldButtonController>().Deselected();
        }*/
    }

    private void Update()
    {
        
    }

    void goHelp()
    {
        SceneManager.LoadScene(5);
    }

    void startClicked()
    {
        rainSounds.StartRain();
        backgroundAnim.SetTrigger("Start");
        rainAnim.SetTrigger("Start");
        cloudAnim.SetTrigger("Start");
        buttonPlay.SetTrigger("Start");
        buttonHelp.SetTrigger("Start");
        buttonStore.SetTrigger("Start");
        buttonExit.SetTrigger("Start");
        //buttonWorld1.GetComponent<Animator>().SetTrigger("Start");
        //buttonWorld2.GetComponent<Animator>().SetTrigger("Start");

    }

    void storeClicked()
    {
        SceneManager.LoadSceneAsync(DataManager.instance.StoreSceneIndex);
    }


    private void world1Selected()
    {
        DataManager.instance.selectedLevel = 0;
        //buttonWorld1.GetComponent<WorldButtonController>().Selected();
        //buttonWorld2.GetComponent<WorldButtonController>().Deselected();

    }

    private void world2Selected()
    {
        DataManager.instance.selectedLevel = 1;
        //buttonWorld2.GetComponent<WorldButtonController>().Selected();
        //buttonWorld1.GetComponent<WorldButtonController>().Deselected();

    }

    public void animationFinished()
    {
        if (DataManager.instance.selectedLevel == 0)
        {
            startEndless1();
        }
        else
        {
            startEndless2();
        }
    }

    void startEndless1()
    {
        SceneManager.LoadSceneAsync(DataManager.instance.endlessSceneIndex);
    }

    void startEndless2()
    {
        SceneManager.LoadScene(DataManager.instance.endlessSceneIndex+1);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
