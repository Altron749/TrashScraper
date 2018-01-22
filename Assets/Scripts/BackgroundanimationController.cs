using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundanimationController : MonoBehaviour {
    public RainController RC;
    public MainMenuController MMC;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DecreaseRainVolume()
    {
        RC.StopRain();
    }

    public void animationFinished()
    {
        MMC.animationFinished();
    }
}
