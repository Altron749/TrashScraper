using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimer : MonoBehaviour {
    public int type;
    private Image timer;
    bool ticking = false;
    float timeLeft;
    float totalTime;

    private void Start()
    {
        timer = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
        if (ticking)
        {
            if (timeLeft<=0)
            {
                ticking= false;
                timeLeft = 0;
            }
            else
            {
                timer.fillAmount = (timeLeft / totalTime);
                timeLeft -= Time.deltaTime;
            }
        }
		
	}


    public void AddTime(float timeToAdd)
    {
        ticking = true;
        timeLeft = timeToAdd;
        totalTime = timeLeft;
    }
}
