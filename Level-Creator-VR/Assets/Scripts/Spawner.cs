using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private bool spawnOnce;
    private bool deleteOnce;
    private bool delete;
    public bool gualdal;
    private bool hasPlayer;
    private GameObject playerObj;
    public List<GameObject> spawnedObjects;
    public GameObject cubePrefab;
    public GameObject leftHand;
    public GameObject lightPrefab;
    public GameObject playerPrefab;
    public GameObject cubeHand, lightHand, playerHand, checkpointHand, finishHand, axisRotatorHand;
    public GameObject testObject;
    public Material cubeMat;
    public Material deleteMat;
    public List<GameObject> selectionText = new List<GameObject>();

    public float timeToSpawn;
    float timeToSpawnInit;
    public bool cube, light, player, checkpoint, finish, axisRotator;

    public LevelLoader levelLoader;
    public UndoRedoManager urManager;

    void Start()
    {
        delete = false;
        cube = true;
        gualdal = false;
        levelLoader = GetComponent<LevelLoader>();
        urManager = GetComponent<UndoRedoManager>();

        timeToSpawnInit = timeToSpawn;
        timeToSpawn= 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cube)
        {
            cubeHand.SetActive(true);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
        }
        else if (light)
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(true);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
        }
        else if (player)
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(true);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
        }
        else if (checkpoint) 
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(true);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
        }
        else if (finish) 
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(true);
            axisRotatorHand.SetActive(false);
        }
        else if (axisRotator) 
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(true);
        }


        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.3f && !spawnOnce)
        {
          
            if(cube)
            {
                if(delete)
                {
                    foreach (GameObject cub in spawnedObjects)
                    {
                        if (cub.transform.position == cubeHand.transform.position)
                        {
                            cub.SetActive(false);
                            urManager.AddAction(new ModuleErase(cub), spawnedObjects);
                            /*
                            Destroy(cub);
                            if (cub.gameObject.tag != "player")
                            {
                                spawnedObjects.Remove(cub);
                            }*/
                        }
                    }
                }
                else
                {
                    if(timeToSpawn <= 0.0f)
                    {
                        bool canSpawn = true;
                        RaycastHit hit;
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.forward), out hit, 1.5f))
                        {
                            if(hit.transform.tag == "cube")
                            {
                                if(hit.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.back), out hit, 1.5f))
                        {
                            if(hit.transform.tag == "cube")
                            {
                                if(hit.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.up), out hit, 1.5f))
                        {
                            if(hit.transform.tag == "cube")
                            {
                                if(hit.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.down), out hit, 1.5f))
                        {
                            if(hit.transform.tag == "cube")
                            {
                                if(hit.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.left), out hit, 1.5f))
                        {
                            if(hit.transform.tag == "cube")
                            {
                                if(hit.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.right), out hit, 1.5f))
                        {
                            if(hit.transform.tag == "cube")
                            {
                                if(hit.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }

                        if(canSpawn)
                        {
                            GameObject g = Instantiate(cubePrefab, cubeHand.transform.position, cubeHand.transform.rotation);
                            spawnedObjects.Add(g);
                            spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
                            timeToSpawn = timeToSpawnInit; //reseteamos la cadencia
                            urManager.AddAction(new ModuleCreate(g), spawnedObjects);
                        }
                        
                    }
                    else
                    {
                        timeToSpawn -= Time.deltaTime;
                    }
                  
                }
            }
            else if(light)
            {
                GameObject g = Instantiate(lightPrefab, lightHand.transform.position, lightHand.transform.rotation);
                spawnedObjects.Add(g);
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
                 spawnOnce = true;
                urManager.AddAction(new ModuleCreate(g), spawnedObjects);
            }
            else if(player && !hasPlayer)
            {
                GameObject playerObject = Instantiate(playerPrefab, playerHand.transform.position, playerHand.transform.rotation);
                spawnedObjects.Add(playerObject);
                urManager.AddAction(new ModuleCreate(playerObject), spawnedObjects);
                playerObj = playerObject;
                hasPlayer = true;
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
                spawnOnce = true;
            }

        }
        else
        {
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") < 0.3f)
            {
                spawnOnce = false;
                timeToSpawn = 0.0f;
            }
                
        }
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") > 0.3f/*& !deleteOnce*/)
        {
            delete = true;
            cubeHand.GetComponent<MeshRenderer>().material = deleteMat;
            deleteOnce = true;
            if(cube)
            {
                for (int i = spawnedObjects.Count; i > 0; i--)
                {
                    if(spawnedObjects[i].transform.position == cubeHand.transform.position)
                    {
                        spawnedObjects[i].gameObject.SetActive(false);
                        urManager.AddAction(new ModuleErase(spawnedObjects[i].gameObject), spawnedObjects);
                        /*spawnedObjects.Remove(spawnedObjects[i].gameObject);
                        Destroy(spawnedObjects[i].gameObject);   */                                         
                    }
                }
            }

        }
        else
        {
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") < 0.3f)
            {
                cubeHand.GetComponent<MeshRenderer>().material = cubeMat;
                delete = false;
            }
        }


        /*foreach(GameObject g in spawnedObjects)
        {
            if(Vector3.Distance(testObject.transform.position, g.transform.position) <= 1)
            {
                
            }
        }*/
    }

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(leftHand.transform.position, leftHand.transform.forward, out hit, 30))
        {
            if (hit.transform.tag == "activateCube")
            {
                selectionText[0].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[0].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "activateCube" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = true;
                light = false;
                player = false;
                checkpoint = false;
                finish = false;
                axisRotator = false;
            }

            if (hit.transform.tag == "activateLight")
            {
                selectionText[2].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[2].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "activateLight" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = false;
                light = true;
                player = false;
                checkpoint = false;
                finish = false;
                axisRotator = false;
            }

            if (hit.transform.tag == "activatePlayer")
            {
                selectionText[1].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[1].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "activatePlayer" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = false;
                light = false;
                checkpoint = false;
                player = true;
                axisRotator = false;
                finish = false;
            }

            // Checkpoint
            if(hit.transform.tag == "activateCheckpoint") 
            {
                selectionText[4].GetComponent<Text>().color = Color.white;
            }
            else 
            {
                selectionText[4].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "activateCheckpoint" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = false;
                light = false;
                player = false;
                checkpoint = true;
                finish = false;
                axisRotator = false;
            }

            // Finish
            if (hit.transform.tag == "activateFinish")
            {
                selectionText[5].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[5].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "activateFinish" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = false;
                light = false;
                player = false;
                checkpoint = false;
                finish = true;
                axisRotator = false;
            }

            // AxisRotator
            if (hit.transform.tag == "activateAxisRotator")
            {
                selectionText[6].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[6].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "activateAxisRotator" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = false;
                light = false;
                player = false;
                checkpoint = false;
                finish = false;
                axisRotator = true;
            }

            // Undo
            if (hit.transform.tag == "undo")
            {
                selectionText[3].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[3].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "undo" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                urManager.Undo();
            }

            // Redo
            if (hit.transform.tag == "redo")
            {
                selectionText[7].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[7].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "redo" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                urManager.Redo();
            }

            // Play
            if (hit.transform.tag == "play")
            {
                selectionText[8].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[8].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "play" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                if(hasPlayer)
                {
                    playerObj.GetComponent<Movement>().Activate();
                }
            }

            // Remove Player
            if (hit.transform.tag == "RemovePlayer")
            {
                selectionText[11].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[11].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "RemovePlayer" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                if (hasPlayer)
                {
                    Destroy(playerObj);
                    hasPlayer = false;
                }
            }

            // Load
            if (hit.transform.tag == "load")
            {
                selectionText[10].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[10].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "load" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                levelLoader.OpenLoadCanvas();
            }

            // Save
            if (hit.transform.tag == "save")
            {
                selectionText[9].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[9].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "save" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f && !gualdal)
            {
                gualdal = true;
                levelLoader.OpenSaveCanvas();
            }
            else if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") < 0.2f)
            {
                gualdal = false;
            }
        }
        
    }
}
