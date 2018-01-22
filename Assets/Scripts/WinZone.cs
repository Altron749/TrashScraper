using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour {
    GameManagerLevels GM;
	// Use this for initialization
	void Start () {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManagerLevels>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            GM.WinLevel();
        }
    }
}
