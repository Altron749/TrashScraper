using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour {

    private VideoPlayer intro;
	// Use this for initialization
	void Start () {
        intro = GetComponent<VideoPlayer>();
        if (!intro.isPlaying)
        {
            intro.Play();
            intro.loopPointReached += GoToMenu;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}



    void GoToMenu(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu");
    }
}
