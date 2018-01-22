using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpBack : MonoBehaviour {

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BackToMenu);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(DataManager.instance.menuSceneIndex);
    }
}
