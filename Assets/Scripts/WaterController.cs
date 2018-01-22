using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

    public float startingSpeed = 1f;
    private float speed;
    public float acceleration = 0.05f;
    public float maxSpeed = 2;
    public float maxDistanceBeforeCatchingUp = 10;
    public float endOfCatchingUp = 5;
    private float speedBeforeCatchingUp;
    private bool catchingUp = false;
    private bool isFrozen = false;
    private readonly int GroundLayer = 9;
    private readonly int WaterLayer = 4;

    public bool isWaterDisabled = false;

    public int numberOfWaves = 3;
    public int shiftLimit = 1;
    public float sizeOfWaterPrefabX;
    private float scaleOfWater=3;
    public GameObject wavePrefab;
    private List<GameObject> waves;
    private float leftBorder;
    private float rightBorder;
    private float sizeX;

    Vector3 up = Vector3.up;
    // Use this for initialization
	void Awake () {
        setStartingSpeed();
        waves = new List<GameObject>();
         sizeX = wavePrefab.GetComponent<Renderer>().bounds.size.x;
        sizeOfWaterPrefabX = sizeX;
        leftBorder = transform.position.x - (numberOfWaves * sizeX) / 2 - sizeX / 2;
        rightBorder = transform.position.x + (numberOfWaves * sizeX) / 2 + sizeX / 2;
        for (int i = 0; i < numberOfWaves; i++)
        {
            waves.Add(Instantiate(wavePrefab, transform.position - new Vector3((numberOfWaves*sizeX)/2-i*sizeX - sizeX/2, 0, 0), Quaternion.identity, transform));
        }
	}

    void Start()
    {
        this.transform.localScale = new Vector3(scaleOfWater,scaleOfWater);

    }

    // Update is called once per frame
    void Update () {

        leftBorder = transform.position.x - (numberOfWaves * sizeX) / 2 - sizeX / 2;
        rightBorder = transform.position.x + (numberOfWaves * sizeX) / 2 + sizeX / 2;
        if (!isWaterDisabled)
        {
            speed = speed + acceleration * Time.deltaTime;
            transform.position = transform.position + speed * up * Time.deltaTime;
        }

        if (Mathf.Abs(leftBorder - Camera.main.transform.position.x) < shiftLimit * sizeOfWaterPrefabX)
        {
            shiftLeft();
        }
        else if (Mathf.Abs(rightBorder - Camera.main.transform.position.x) < shiftLimit * sizeOfWaterPrefabX)
        {
            shiftRight();
        }

        if (catchingUp)
        {
            if (-Camera.main.ScreenToWorldPoint(Vector3.zero).y + transform.position.y > -endOfCatchingUp)
            {
                catchingUp = false;
                EndCatchingUp();
            }
            CatchUp();

        }
        else
        {
            if (Camera.main.ScreenToWorldPoint(Vector3.zero).y - transform.position.y > maxDistanceBeforeCatchingUp)
            {
                catchingUp = true;
                speedBeforeCatchingUp = speed;
            }
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W pressed");
            ChangeWaterDisabled();
        }

    }

    public void setStartingSpeed()
    {
        speed = startingSpeed;
    }

    private void shiftRight()
    {
        this.transform.position += new Vector3(sizeOfWaterPrefabX*scaleOfWater, 0);

        //GameObject temp = waves[0];
        //waves.RemoveAt(0);
        //temp.transform.position +=new Vector3((numberOfWaves)* sizeOfWaterPrefabX,0);
        //rightBorder += sizeOfWaterPrefabX;
        //leftBorder += sizeOfWaterPrefabX;
        //waves.Add(temp);

    }

    private void shiftLeft()
    {
        this.transform.position -= new Vector3(sizeOfWaterPrefabX*scaleOfWater,0);
        //GameObject temp = waves[waves.Count-1];
        //waves.RemoveAt(waves.Count - 1);
        //temp.transform.position -= new Vector3((numberOfWaves) * sizeOfWaterPrefabX, 0);
        //rightBorder -= sizeOfWaterPrefabX;
        //leftBorder -= sizeOfWaterPrefabX;
        //waves.Insert(0,temp);

    }

    private void EndCatchingUp()
    {
        speed = speedBeforeCatchingUp + (speedBeforeCatchingUp-startingSpeed) * 1 / 2;
    }

    private void CatchUp()
    {
        Vector3 toMove = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(Vector3.zero).y - 2, 0);
        transform.position =  toMove;
    }

    public void Freeze()
    {
        isFrozen = true;
        for (int i = 0; i < waves.Count; i++)
        {
            //set layer of the water to ground so jump can reset when standing on the frozen water
            waves[i].GetComponent<Collider2D>().isTrigger = false;
            waves[i].layer = GroundLayer;
            waves[i].GetComponent<Animator>().SetBool("Frozen",true);
        }
    }

    public void Unfreeze()
    {
        isFrozen = false;
        for (int i = 0; i < waves.Count; i++)
        {
            //revert layer of the water to normal
            waves[i].GetComponent<Collider2D>().isTrigger = true;
            waves[i].layer = WaterLayer;
            waves[i].GetComponent<Animator>().SetBool("Frozen", false);
        }
    }

    public bool isWaterFrozen()
    {
        return isFrozen;
    }

    public void ChangeWaterDisabled()
    {
        isWaterDisabled = !isWaterDisabled;
    }
}
