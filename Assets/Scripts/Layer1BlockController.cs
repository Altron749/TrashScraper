using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer1BlockController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sortingLayerName = "Enviroment layer 1";

    }

    // Update is called once per frame
    void Update () {
		
	}
}
