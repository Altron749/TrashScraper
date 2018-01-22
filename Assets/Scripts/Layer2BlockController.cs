using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer2BlockController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sortingLayerName = "Enviroment layer 2";
            }
	
	// Update is called once per frame
	void Update () {
		
	}
}
