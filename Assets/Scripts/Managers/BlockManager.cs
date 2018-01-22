using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

    public List<GameObject> buildingBlockPrefabs;
    public List<GameObject> blocks;


    //lists of prefabs
    public List<GameObject> doorPrefabs;
    public List<GameObject> platformPrefabs;
    public List<GameObject> basicBlockPrefabs;
    public List<GameObject> borderPrefabs;
    public List<GameObject> borderWidePrefabs;
    public List<GameObject> roofPrefabs;
    public List<GameObject> supportPrefabs;
    public List<GameObject> collectiblesPrefabs;
    public List<GameObject> layer1Prefabs;
    public List<GameObject> layer2Prefabs;
    public List<GameObject> powerUpPrefabs;

    public GameObject emptyBlockPrefab;

    public int maximalDistanceToGenerate = 20;



    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].GetComponent<BlockScript>().Generate(doorPrefabs, platformPrefabs, basicBlockPrefabs, layer1Prefabs, layer2Prefabs, borderPrefabs, borderWidePrefabs, collectiblesPrefabs, powerUpPrefabs, roofPrefabs, supportPrefabs,this);
        }

    }

    // Update is called once per frame
    void Update () {
        GameObject lastBlock = blocks[blocks.Count - 1];
        float positionY = lastBlock.transform.position.y;
        if (positionY - Camera.main.transform.position.y < maximalDistanceToGenerate)
        {
            GenerateNewBlock();
        }
    }

    public void GenerateEmptyBlock()
    {

    }

    public void GenerateNewBlock()
    {
        float lastPositionX;
        float positionY;
        GameObject toMake = null;
        bool selected = false;
        GameObject lastBlock = blocks[blocks.Count - 1];
        lastPositionX = lastBlock.transform.position.x;
        positionY = lastBlock.transform.position.y;
        while (!selected)
        {
            selected = true;
            if (positionY >= -6)
            {
                int randomBlock = Random.Range(0, buildingBlockPrefabs.Count);
                toMake = buildingBlockPrefabs[randomBlock];
                float rnd = Random.Range(0.5f, 1.0f) * toMake.GetComponent<BlockScript>().difficulty;
                if (rnd > Mathf.Max(0, positionY) / 50)
                {
                    selected = false;
                }
            }
            else
            {
                toMake = emptyBlockPrefab;
            }

            //if (toMake.GetComponent<BlockScript>().difficulty<1 && positionY>100)
            //{
            //    selected = false;
            //}

        }


        float sizey = lastBlock.GetComponent<BlockScript>().heigth;
        //create an empty, which will then generate itself
        GameObject temp = Instantiate(toMake, new Vector3(lastPositionX, positionY + sizey, 0), Quaternion.identity);
        //generates itself, the position is based on the position of the parent
        temp.GetComponent<BlockScript>().Generate(doorPrefabs, platformPrefabs, basicBlockPrefabs, layer1Prefabs, layer2Prefabs,borderPrefabs, borderWidePrefabs, collectiblesPrefabs,powerUpPrefabs, roofPrefabs, supportPrefabs,this);

        blocks.Add(temp);
    }

    public bool IsPlatformUnder(int index)
    {
        if (blocks.Count>=2)
        {
            BlockScript toEvaluate = blocks[blocks.Count - 1].GetComponent<BlockScript>();
            if (index < 0 || index >= toEvaluate.buildingWidth) 
            {
                return false;
            }
            else
            {
                return toEvaluate.IsPlatformOnLastFloorOnIndex(index);
            }

        }
        else
        {
            return false;
        }
    }
}
