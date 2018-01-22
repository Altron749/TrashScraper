using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextController : MonoBehaviour {


    Text scoreText;
    bool animate=false;
    float animationTimeCounter;
    float animationDuration = 1;
    Color greeny;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
        greeny = scoreText.color;
    }

    private void Update()
    {
        animationTimeCounter += Time.deltaTime;
        if (animate)
        {
            if (animationTimeCounter>animationDuration)
            {
                animate = false;
            }
            else if ((int)(animationTimeCounter * 8) % 2 == 1)
            {
                scoreText.color = Color.red;
            }
            else
            {
                scoreText.color = greeny;
            }
        }
        else
        {
            scoreText.color = greeny;
        }
    }

    public void SetScore(int score)
    {
        animationTimeCounter = 0;
        scoreText.text =  score.ToString();
        animate = true;
    }
}
