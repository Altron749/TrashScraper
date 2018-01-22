using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform player;
    public Transform cameraTransform;
    public float cameraMoveSpeed;
    private Vector3 toGo;
    public int sizeForAndroid = 18;
    public int sizeForPC = 18;
    private bool deadAnimation = false;
    private int sizeZoomedIn = 6;
    private float zoomInSpeed = 5;
    private Camera thisCamera;
	// Use this for initialization
	void Start () {
        thisCamera = GetComponent<Camera>();
        cameraTransform = GetComponent<Transform>();
#if UNITY_ANDROID
            thisCamera.orthographicSize = sizeForAndroid;
#else
        thisCamera.orthographicSize = sizeForPC;
#endif
    }
	
	// Update is called once per frame
	void Update () {
        toGo = player.position;
        toGo.z = transform.position.z;
        transform.position = toGo;
        if (deadAnimation)
        {
            zoomIn();
        }
	}

    public void Dead()
    {
        deadAnimation = true;
    }

    private void zoomIn()
    {
        if (thisCamera.orthographicSize>sizeZoomedIn)
        {
            thisCamera.orthographicSize -= zoomInSpeed * Time.deltaTime;
        }
    }
}
