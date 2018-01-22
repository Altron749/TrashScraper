using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
    private BlockManager BM;
    public GameManagerEndless GM;
    public List<GameObject> possibleCollectiblePlaces;
    public float width;
    public float heigth;

    public int[] platforms;

    public int difficulty;

    private float blockWidth;
    private float blockHeigth;

    public int generatedBlockHeigth = 2;
    public int buildingWidth = 5;

    private bool shifted = false;
    private bool right ;



    // Use this for initialization
    void Awake () {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManagerEndless>();
        //platforms = new platformFloorPositions[generatedBlockHeigth];
//        Generate();
    }



    public bool IsPlatformOnLastFloorOnIndex(int index)
    {
        if (platforms[(generatedBlockHeigth-1)*buildingWidth+index]==1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //TODO: je potřeba dodělat generování trochu do stran + přístavby, dále je potřeba udělat checky splnitelnosti, zvyšující se obtížnost
    public void Generate(List<GameObject> doorPrefabs, List<GameObject> platformPrefabs,List<GameObject> basicBlockPrefabs, List<GameObject> layer1Prefabs, List<GameObject> layer2Prefabs, List<GameObject> borders, List<GameObject> bordersWide, List<GameObject> collectiblesPrefabs, List<GameObject> powerUpPrefabs, List<GameObject> roofPrefabs, List<GameObject> supportPrefabs, BlockManager BM)
    {
        this.BM = BM;
        blockWidth = basicBlockPrefabs[0].GetComponent<Renderer>().bounds.size.x;
        blockHeigth = basicBlockPrefabs[0].GetComponent<Renderer>().bounds.size.y;
        width = blockWidth * buildingWidth;
        heigth = blockHeigth * generatedBlockHeigth;

        Shift(roofPrefabs,supportPrefabs);
        GenerateBackground(basicBlockPrefabs);
        GeneratePlatforms(platformPrefabs);
        GenerateLayer1(layer1Prefabs, borders, bordersWide);
        GenerateLayer2(doorPrefabs, layer2Prefabs);
        GenerateCollectibles(collectiblesPrefabs,powerUpPrefabs);
    }

    /// <summary>
    /// Generates background, so far only on rectangle for each place selects randomly block
    /// </summary>
    public void GenerateBackground(List<GameObject> basicBlockPrefabs)
    {
        for (int i = 0; i < generatedBlockHeigth; i++)
        {
            for (int j = 0; j < buildingWidth; j++)
            {
                GameObject blockToMake = basicBlockPrefabs[Random.Range(0, basicBlockPrefabs.Count)];
                //positiono generated goes up from the parent gameobject
                GameObject temp = Instantiate(blockToMake, transform.position + new Vector3(j * blockWidth - width/2 + blockWidth/2, i * blockHeigth + blockHeigth/2 ), Quaternion.identity, transform);
            }
        }
    }

    public void GenerateLayer1(List<GameObject> layer1Prefabs, List<GameObject> borders, List<GameObject> bordersWide)
    {
        for (int i = 0; i < generatedBlockHeigth; i++)
        {
            for (int j = 0; j < buildingWidth; j++)
            {
                if (Random.Range(0f, 1) > 0.5f)
                {
                    GameObject layer1BlockToMake = layer1Prefabs[Random.Range(0, layer1Prefabs.Count)];
                    //positiono generated goes up from the parent gameobject
                    GameObject temp = Instantiate(layer1BlockToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);
                }

                if (j==0)
                {
                    if (Random.Range(0f,1)>0.6f)
                    {
                        if (Random.Range(0f, 1) > 0.5f)
                        {
                            if (borders.Count>0)
                            {
                                GameObject borderToMake = borders[Random.Range(0, borders.Count)];
                                //positiono generated goes up from the parent gameobject
                                GameObject temp = Instantiate(borderToMake, transform.position + new Vector3(-width / 2 - blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);
                            }
                        }
                        else
                        {
                            if (bordersWide.Count>0)
                            {
                                GameObject borderToMake = bordersWide[Random.Range(0, bordersWide.Count)];
                                //positiono generated goes up from the parent gameobject
                                GameObject temp = Instantiate(borderToMake, transform.position + new Vector3(-width / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);
                                temp.transform.localScale = (new Vector3(-temp.transform.localScale.x, temp.transform.localScale.y, temp.transform.localScale.z));
                            }

                        }
                    }
                }

                if (j==buildingWidth-1)
                {
                    if (Random.Range(0f, 1) > 0.6f)
                    {
                        if (Random.Range(0f, 1) > 0.5f)
                        {
                            if (borders.Count > 0)
                            {

                                GameObject borderToMake = borders[Random.Range(0, borders.Count)];
                                //positiono generated goes up from the parent gameobject
                                GameObject temp = Instantiate(borderToMake, transform.position + new Vector3(width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);
                                temp.transform.localScale = (new Vector3(-temp.transform.localScale.x, temp.transform.localScale.y, temp.transform.localScale.z));

                            }
                        }
                        else
                        {
                            if (bordersWide.Count > 0)
                            {

                            GameObject borderToMake = bordersWide[Random.Range(0, bordersWide.Count)];
                            //positiono generated goes up from the parent gameobject
                            GameObject temp = Instantiate(borderToMake, transform.position + new Vector3(width / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);
                        }
                        }
                    }
                }
            }
        }
    }

    public void GenerateLayer2(List<GameObject> doorPrefabs, List<GameObject> layer2Prefabs)
    {
        for (int i = 0; i < generatedBlockHeigth; i++)
        {
            for (int j = 0; j < buildingWidth; j++)
            {
                if (Random.Range(0f, 1) > 0.5f)
                {
                    if (i == 0)
                    {
                        int index = j;
                        if (shifted)
                        {
                            if (right)
                            {
                                index++;
                            }
                            else
                            {
                                index--;
                            }

                        }

                        if (BM.IsPlatformUnder(index))
                        {
                            if (Random.Range(0f, 1) > 0.15f)
                            {
                                int toMakeIndex = Random.Range(0, doorPrefabs.Count);
                                GameObject doorToMake = doorPrefabs[toMakeIndex];
                                //positiono generated goes up from the parent gameobject
                                GameObject temp = Instantiate(doorToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);

                            }
                            else
                            {
                                int toMakeIndex = Random.Range(0, layer2Prefabs.Count);
                                GameObject layer2BlockToMake = layer2Prefabs[toMakeIndex];
                                //positiono generated goes up from the parent gameobject
                                GameObject temp = Instantiate(layer2BlockToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);

                            }
                        }
                        else
                        {
                            int toMakeIndex = Random.Range(0, layer2Prefabs.Count);
                            GameObject layer2BlockToMake = layer2Prefabs[toMakeIndex];
                            //positiono generated goes up from the parent gameobject
                            GameObject temp = Instantiate(layer2BlockToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);

                        }
                    }
                    else
                    { 
                    if (platforms[(i-1) * buildingWidth + j] != 0)
                    {
                        if (Random.Range(0f, 1) > 0.25f)
                        {
                                int toMakeIndex = Random.Range(0, doorPrefabs.Count);
                                GameObject doorToMake = doorPrefabs[toMakeIndex];
                                //positiono generated goes up from the parent gameobject
                                GameObject temp = Instantiate(doorToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);

                            }
                            else
                        {
                                int toMakeIndex = Random.Range(0, layer2Prefabs.Count);
                                GameObject layer2BlockToMake = layer2Prefabs[toMakeIndex];
                                //positiono generated goes up from the parent gameobject
                                GameObject temp = Instantiate(layer2BlockToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);
                        }
                    }
                        else
                        {
                            int toMakeIndex = Random.Range(0, layer2Prefabs.Count);
                            GameObject layer2BlockToMake = layer2Prefabs[toMakeIndex];
                            //positiono generated goes up from the parent gameobject
                            GameObject temp = Instantiate(layer2BlockToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);

                        }
                    }
  
                }
            }
        }
    }

    public void GeneratePlatforms(List<GameObject> platformPrefabs)
    {

        for (int i = 0; i < generatedBlockHeigth; i++)
        {
            for (int j = 0; j < buildingWidth; j++)
            {
                if (platforms[i*buildingWidth+j]!=0)
                {
                    GameObject platformToMake = platformPrefabs[0];
                    GameObject temp = Instantiate(platformToMake, transform.position + new Vector3(j * blockWidth - width / 2 + blockWidth / 2, i * blockHeigth + blockHeigth / 2), Quaternion.identity, transform);

                }
            }
        }
    }

    private void GenerateCollectibles(List<GameObject> collectiblesPrefabs,List<GameObject> powerUpPrefabs)
    {
        foreach (GameObject collectiblePlace in possibleCollectiblePlaces)
        {
            float rnd = Random.Range(0, 1f);
            if (rnd > 0.95)
            {
                GameObject toGenerate = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];
                Instantiate(toGenerate, collectiblePlace.transform);
            }
            else if ( rnd > 0.7f)
            {
                GameObject toGenerate = collectiblesPrefabs[Random.Range(0, collectiblesPrefabs.Count)];
                Instantiate(toGenerate, collectiblePlace.transform);
            }
        }
        
    }
    private void Shift(List<GameObject> roofPrefabs, List<GameObject> supportPrefabs)
    {
        if (Random.Range(0f,1f)>0.75f)
        {
            shifted = true;
            right = (Random.Range(0f, 1f) > 0.5);
            if (right)
            {
                transform.position = (transform.position + new Vector3(blockWidth, 0, 0));
                GameObject supportToMake = supportPrefabs[Random.Range(0, supportPrefabs.Count)];
                GameObject supportTmp = Instantiate(supportToMake, transform.position + new Vector3(width / 2 - blockWidth / 2, -blockHeigth / 2), Quaternion.identity, transform);
                supportTmp.transform.localScale = new Vector3(-supportTmp.transform.localScale.x, supportTmp.transform.localScale.y, supportTmp.transform.localScale.z);


                if (roofPrefabs.Count>0)
                {
                    GameObject roofToMake = roofPrefabs[Random.Range(0, roofPrefabs.Count)];
                    GameObject roofTmp = Instantiate(roofToMake, transform.position + new Vector3(-width / 2 - blockWidth / 2, blockHeigth / 2), Quaternion.identity, transform);
                    roofTmp.transform.localScale = new Vector3(-roofTmp.transform.localScale.x, roofTmp.transform.localScale.y, roofTmp.transform.localScale.z);

                }
            }
            else
            {
                transform.position = (transform.position - new Vector3(blockWidth, 0, 0));
                if (roofPrefabs.Count>0)
                {
                    GameObject roofToMake = roofPrefabs[Random.Range(0, roofPrefabs.Count)];
                    Instantiate(roofToMake, transform.position + new Vector3(width / 2 + blockWidth / 2, blockHeigth / 2), Quaternion.identity, transform);

                }
                GameObject supportToMake = supportPrefabs[Random.Range(0, supportPrefabs.Count)];
                GameObject supportTmp = Instantiate(supportToMake, transform.position + new Vector3(-width / 2 + blockWidth / 2, -blockHeigth / 2), Quaternion.identity, transform);
            }
        }
    }
}
