using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private bool spawnOnce;
    private bool deleteOnce;
    private bool delete;
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

    public bool cube, light, player, checkpoint, finish, axisRotator;

    private LevelLoader levelLoader;
    private UndoRedoManager urManager;

    void Start()
    {
        delete = false;
        cube = true;
        levelLoader = GetComponent<LevelLoader>();
        urManager = GetComponent<UndoRedoManager>();
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
            spawnOnce = true;

            if(cube)
            {
                if(delete)
                {
                    if (cube)
                    {
                        foreach (GameObject cub in spawnedObjects)
                        {
                            if (cub.transform.position == cubeHand.transform.position)
                            {
                                Destroy(cub);
                                spawnedObjects.Remove(cub);

                            }
                        }
                    }
                }
                else
                {
                    spawnedObjects.Add(Instantiate(cubePrefab, cubeHand.transform.position, cubeHand.transform.rotation));
                    spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
                }
            }
            else if(light)
            {
                spawnedObjects.Add(Instantiate(lightPrefab, lightHand.transform.position, lightHand.transform.rotation));
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
            }
            else if(player)
            {
                spawnedObjects.Add(Instantiate(playerPrefab, playerHand.transform.position, playerHand.transform.rotation));
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
            }

        }
        else
        {
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") < 0.3f)
                spawnOnce = false;
        }
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") > 0.3f/*& !deleteOnce*/)
        {
            delete = true;
            cubeHand.GetComponent<MeshRenderer>().material = deleteMat;
            deleteOnce = true;
            if(cube)
            {
                foreach (GameObject cub in spawnedObjects)
                {
                    if(cub.transform.position == cubeHand.transform.position)
                    {
                        Destroy(cub);
                        spawnedObjects.Remove(cub);
                        
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
            if (hit.transform.tag == "undo")
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
                //levelLoader.Load();
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
            if (hit.transform.tag == "save" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                levelLoader.Save();
            }
        }
        
    }
}
