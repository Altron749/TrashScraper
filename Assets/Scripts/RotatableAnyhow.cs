using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableAnyhow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int rotX = Random.Range(-1,1);
        int rotY = Random.Range(-1, 1);
        transform.localScale = new Vector3(rotX,rotY);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
