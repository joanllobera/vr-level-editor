using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private bool spawnOnce;
    private bool deleteOnce;
    private bool undoOnce;
    private bool redoOnce;
    private bool delete;
    public bool gualdal;
    private bool hasPlayer;
    private GameObject playerObj;
    public List<GameObject> spawnedObjects;
    public GameObject cubePrefab;
    public GameObject leftHand;
    public GameObject lightPrefab;
    public GameObject playerPrefab;
    public GameObject gapPastPrefab;
    public GameObject gapPresentPrefab;
    public GameObject axisRotatorPrefab;
    public GameObject cubeHand, lightHand, playerHand, checkpointHand, finishHand, axisRotatorHand, gapPastHand, gapPresentHand;
    public GameObject testObject;
    public GameObject cam;
    public Material cubeMat;
    public GameObject canvas;
    public Material deleteMat;
    public List<GameObject> selectionText = new List<GameObject>();

    public float timeToSpawn;
    float timeToSpawnInit;
    public bool cube, light, player, checkpoint, finish, axisRotator, gapPast, gapPresent;

    public LevelLoader levelLoader;
    public UndoRedoManager urManager;

    private int previousButtonIndex;

    void Start()
    {
        delete = false;
        cube = true;
        gualdal = false;
        levelLoader = GetComponent<LevelLoader>();
        urManager = GetComponent<UndoRedoManager>();
        StartCoroutine(CheckPlayer());
        timeToSpawnInit = timeToSpawn;
        timeToSpawn= 0.0f;
        previousButtonIndex = 0;
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
            gapPastHand.SetActive(false);
            gapPresentHand.SetActive(false);
        }
        else if (light)
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(true);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
            gapPastHand.SetActive(false);
            gapPresentHand.SetActive(false);
        }
        else if (player)
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(true);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
            gapPastHand.SetActive(false);
            gapPresentHand.SetActive(false);
        }
        else if (checkpoint) 
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(true);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
            gapPastHand.SetActive(false);
            gapPresentHand.SetActive(false);
        }
        else if (finish) 
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(true);
            axisRotatorHand.SetActive(false);
            gapPastHand.SetActive(false);
            gapPresentHand.SetActive(false);
        }
        else if (axisRotator) 
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(true);
            gapPastHand.SetActive(false);
            gapPresentHand.SetActive(false);
        }
        else if (gapPresent)
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
            gapPastHand.SetActive(false);
            gapPresentHand.SetActive(true);
        }
        else if (gapPast)
        {
            cubeHand.SetActive(false);
            lightHand.SetActive(false);
            playerHand.SetActive(false);
            checkpointHand.SetActive(false);
            finishHand.SetActive(false);
            axisRotatorHand.SetActive(false);
            gapPastHand.SetActive(true);
            gapPresentHand.SetActive(false);
        }

        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.3f && !spawnOnce)
        {
          
            if(cube)
            {
                if(delete)
                {
                    foreach (GameObject cub in spawnedObjects)
                    {
                        if(cub.gameObject.tag != "Player")
                        {
                            if(cub.transform.position == cubeHand.transform.position)
                            {
                                cub.SetActive(false);
                                urManager.AddAction(new ModuleErase(cub), spawnedObjects);
                            }
                        }
                        else
                        {
                            if(Vector3.Distance(cub.transform.position, cubeHand.transform.position) <= 0.5f)
                            {
                                cub.SetActive(false);
                                urManager.AddAction(new ModuleErase(cub), spawnedObjects);
                            }
                        }
                    }
                }
                else
                {
                    if(timeToSpawn <= 0.0f)
                    {
                        bool canSpawn = true;
                        
                        RaycastHit hit2;
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.forward), out hit2, 1.5f))
                        {
                            if(hit2.transform.tag == "cube")
                            {
                                if(hit2.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.back), out hit2, 1.5f))
                        {
                            if(hit2.transform.tag == "cube")
                            {
                                if(hit2.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.up), out hit2, 1.5f))
                        {
                            if(hit2.transform.tag == "cube")
                            {
                                if(hit2.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.down), out hit2, 1.5f))
                        {
                            if(hit2.transform.tag == "cube")
                            {
                                if(hit2.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.left), out hit2, 1.5f))
                        {
                            if(hit2.transform.tag == "cube")
                            {
                                if(hit2.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }
                        if(Physics.Raycast(cubeHand.transform.position, cubeHand.transform.TransformDirection(Vector3.right), out hit2, 1.5f))
                        {
                            if(hit2.transform.tag == "cube")
                            {
                                if(hit2.transform.gameObject.GetComponent<BifurcationAvoider>().raycastCount > 1)
                                {
                                    canSpawn = false;
                                }
                            }
                        }

                        if(canSpawn)
                        {
                            GameObject g = Instantiate(cubePrefab, cubeHand.transform.position, cubeHand.transform.rotation);
                            spawnedObjects.Add(g);
                            //spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
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
            else if(gapPast)
            {
                GameObject g = Instantiate(gapPastPrefab, gapPastHand.transform.position, gapPastHand.transform.rotation);
                spawnedObjects.Add(g);                
                spawnOnce = true;
                urManager.AddAction(new ModuleCreate(g), spawnedObjects);
            }
            else if(gapPresent)
            {
                GameObject g = Instantiate(gapPresentPrefab, gapPresentHand.transform.position, gapPresentHand.transform.rotation);
                spawnedObjects.Add(g);
                spawnOnce = true;
                urManager.AddAction(new ModuleCreate(g), spawnedObjects);
            }
            else if(player && !hasPlayer)
            {                
                GameObject playerObject = Instantiate(playerPrefab, playerHand.transform.position, playerHand.GetComponent<Player_Hand_Behaviour>().rotation);
                spawnedObjects.Add(playerObject);
                urManager.AddAction(new ModuleCreate(playerObject), spawnedObjects);
                playerObj = playerObject;
                hasPlayer = true;
                //spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
                spawnOnce = true;
            }
            else if(axisRotator)
            {
                bool isOnCorner = false;

                RaycastHit hit2;
                if(Physics.Raycast(axisRotatorHand.transform.position, axisRotatorHand.transform.TransformDirection(Vector3.forward), out hit2, 1.5f))
                {
                    if(hit2.transform.tag == "cube")
                    {
                        isOnCorner = hit2.transform.gameObject.GetComponent<BifurcationAvoider>().isNextToCorner;
                    }
                }
                if(Physics.Raycast(axisRotatorHand.transform.position, axisRotatorHand.transform.TransformDirection(Vector3.back), out hit2, 1.5f))
                {
                    if(hit2.transform.tag == "cube")
                    {
                        isOnCorner = hit2.transform.gameObject.GetComponent<BifurcationAvoider>().isNextToCorner;
                    }
                }
                if(Physics.Raycast(axisRotatorHand.transform.position, axisRotatorHand.transform.TransformDirection(Vector3.up), out hit2, 1.5f))
                {
                    if(hit2.transform.tag == "cube")
                    {
                        isOnCorner = hit2.transform.gameObject.GetComponent<BifurcationAvoider>().isNextToCorner;
                    }
                }
                if(Physics.Raycast(axisRotatorHand.transform.position, axisRotatorHand.transform.TransformDirection(Vector3.down), out hit2, 1.5f))
                {
                    if(hit2.transform.tag == "cube")
                    {
                        isOnCorner = hit2.transform.gameObject.GetComponent<BifurcationAvoider>().isNextToCorner;
                    }
                }
                if(Physics.Raycast(axisRotatorHand.transform.position, axisRotatorHand.transform.TransformDirection(Vector3.left), out hit2, 1.5f))
                {
                    if(hit2.transform.tag == "cube")
                    {
                        isOnCorner = hit2.transform.gameObject.GetComponent<BifurcationAvoider>().isNextToCorner;
                    }
                }
                if(Physics.Raycast(axisRotatorHand.transform.position, axisRotatorHand.transform.TransformDirection(Vector3.right), out hit2, 1.5f))
                {
                    if(hit2.transform.tag == "cube")
                    {
                        isOnCorner = hit2.transform.gameObject.GetComponent<BifurcationAvoider>().isNextToCorner;
                    }
                }

                if(isOnCorner)
                {
                    GameObject axisRot = Instantiate(axisRotatorPrefab, axisRotatorHand.transform.position, lightHand.transform.rotation);
                    spawnedObjects.Add(axisRot);
                    urManager.AddAction(new ModuleCreate(axisRot), spawnedObjects);
                    spawnOnce = true;
                }
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

        if(Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0.3f)
        {
            canvas.transform.position = cam.transform.position + cam.transform.forward * 23 + new Vector3(2, -2, 0);
            canvas.transform.LookAt(2 * canvas.transform.position - cam.transform.position);
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
                gapPast = false;
                gapPresent = false;

                /*for(int i = 0; i < selectionText.Count; i++)
                {
                    selectionText[i].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                }*/

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[0].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 0;
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
                gapPast = false;
                gapPresent = false;

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[2].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 2;
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
                gapPast = false;
                gapPresent = false;

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[1].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 1;
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
                gapPast = false;
                gapPresent = false;

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[4].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 4;
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
                gapPast = false;
                gapPresent = false;

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[5].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 5;
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
                gapPast = false;
                gapPresent = false;

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[6].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 6;
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
            if (hit.transform.tag == "undo" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f/* && !undoOnce*/)
            {
                urManager.Undo();
                undoOnce = true;
                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[3].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 3;
            }
            else if(Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") < 0.3f && undoOnce)
            {
                undoOnce = false;
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
            if (hit.transform.tag == "redo" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f /*&& !redoOnce*/)
            {
                urManager.Redo();
                redoOnce = true;
                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[7].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 7;
            }
            else if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") < 0.3f && redoOnce)
            {
                redoOnce = false;
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

                GameObject[] gaps = GameObject.FindGameObjectsWithTag("gap");
                foreach (GameObject gap in gaps)
                {
                    gap.GetComponent<OverlapCheck>().GetLight();
                }

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[8].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 8;
            }

            //GapPast
            if (hit.transform.tag == "gappast")
            {
                selectionText[13].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[13].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "gappast" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = false;
                light = false;
                player = false;
                checkpoint = false;
                finish = false;
                axisRotator = false;
                gapPast = true;
                gapPresent = false;

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[8].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 13;
            }

            //GapPresent
            if (hit.transform.tag == "gappresent")
            {
                selectionText[14].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[14].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "gappresent" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                cube = false;
                light = false;
                player = false;
                checkpoint = false;
                finish = false;
                axisRotator = false;
                gapPast = false;
                gapPresent = true;

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[4].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 14;
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

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[11].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 11;
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

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[10].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 10;
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
                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[9].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 9;

            }
            else if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") < 0.2f)
            {
                gualdal = false;
            }
            if (hit.transform.tag == "stop")
            {
                selectionText[12].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[12].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "stop" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                if (hasPlayer)
                {
                    playerObj.GetComponent<Movement>().Deactivate();
                }

                //Poner el botón de color amarillo y restablecer el color del botón previamente pulsado
                selectionText[previousButtonIndex].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                selectionText[12].GetComponent<ButtonController>().changeColor(hit.transform.tag);
                previousButtonIndex = 12;
            }
        }
        
    }

    IEnumerator CheckPlayer()
    {
        while(true)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                hasPlayer = false;
            }
            else
            {
                hasPlayer = true;
            }
            Debug.Log(hasPlayer);
            yield return new WaitForSeconds(0.2f);
        }

    }
}
