using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    public GameObject underWaterPrefab;
    public List<GameObject> underwater;
    public int numberOfBlockVerticall = 10;
    private float sizeY1;
    private float sizeY2;


    // Use this for initialization
    void Awake () {
        //top of water is higher than underwater block, so we need to have both sizes
        sizeY1 = underWaterPrefab.GetComponent<Renderer>().bounds.size.y;
        sizeY2 = this.GetComponent<Renderer>().bounds.size.y;

        for (int i = 0; i < numberOfBlockVerticall; i++)
        {
            underwater.Add(Instantiate(underWaterPrefab, transform.position - new Vector3(0, sizeY2/2 + sizeY1/2+(i) * sizeY1), Quaternion.identity, transform));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
