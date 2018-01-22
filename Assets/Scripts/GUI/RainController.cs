using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

    public AudioClip rain;
    AudioSource source;
    bool raining = false;
    float change = 0.1f;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (raining)
        {
            source.volume += Time.deltaTime * change;
        }
	}

    public void StartRain()
    {
        raining = true;
        source.volume = 0;
        source.clip = rain;
        source.Play();
    }

    public void StopRain()
    {
        change = -0.4f;
    }

}
