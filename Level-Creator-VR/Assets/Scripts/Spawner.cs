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

    public Color blueCol;
    public List<GameObject> selectionText = new List<GameObject>();

    public GameObject tooltipText;

    public float timeToSpawn;
    public bool cube, light, player, checkpoint, finish, axisRotator, gapPast, gapPresent;
    private GameObject directionalLight; //para tener una referencia directa a la luz

    public GameObject levelParent, lightCanvas, canvasImage;
    public float timeToSpawnInit, rotLightSpeed;

    public LevelLoader levelLoader;
    public UndoRedoManager urManager;

    private int previousButtonIndex;

    private string cubeHelp, pastCubeHelp, futureCubeHelp, dirLightHelp, checkPointHelp, characterHelp, undoHelp, redoHelp, portalHelp, axisRotatorHelp, playHelp, stopHelp, saveHelp, loadHelp, mobileHelp, removeCharacterHelp;

    public GameObject helpImg;
    public Sprite defaultIMG, characterIMG, cubeIMG, pastCubeIMG, futureCubeIMG, lightIMG, rotatorIMG;
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

          if(directionalLight != null)
            lightCanvas.SetActive(true);
        else
            lightCanvas.SetActive(false);

        cubeHelp = "Cube - Use this to build the basic geometry of the level";
        pastCubeHelp = "Past Cube - Past cubes, only visible in the ";
        futureCubeHelp = "Future Cube - Future cubes, only visible in the ";
        dirLightHelp = "Directional Light - Use this to place a directional light in the level";
        checkPointHelp = "Checkpoint - Place a checkpoint within the level";
        characterHelp = "Character - Use this to place the character in the level";
        undoHelp = "Undo - Reverse the last command executed";
        redoHelp = "Redo - Repeat your previous command";
        portalHelp = "Portal - Place a portal inside the level";
        axisRotatorHelp = "Axis Rotator - ";
        playHelp = "Play - Start playing your level";
        stopHelp = "Stop - Pause the gameplay";
        saveHelp = "Save - Save your current level progress";
        loadHelp = "Load - Load a saved level layout";
        mobileHelp = "Mobile View - Swap to mobile view";
        removeCharacterHelp = "Remove Character - Removes the character from the current level";

        helpImg.GetComponent<Image>().sprite = defaultIMG;

        cubeHand.transform.parent = levelParent.transform;
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
                            spawnedObjects[spawnedObjects.Count - 1].transform.parent = levelParent.transform;
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
                directionalLight = g;
                spawnedObjects.Add(g);
                lightCanvas.SetActive(true);
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano

                spawnOnce = true;
                urManager.AddAction(new ModuleCreate(g), spawnedObjects);
            }
            else if(gapPast)
            {
                GameObject g = Instantiate(gapPastPrefab, gapPastHand.transform.position, gapPastHand.transform.rotation);
                spawnedObjects.Add(g);  
                 spawnedObjects[spawnedObjects.Count - 1].transform.parent = levelParent.transform;              
                spawnOnce = true;
                urManager.AddAction(new ModuleCreate(g), spawnedObjects);
            }
            else if(gapPresent)
            {
                GameObject g = Instantiate(gapPresentPrefab, gapPresentHand.transform.position, gapPresentHand.transform.rotation);
                spawnedObjects.Add(g);
                 spawnedObjects[spawnedObjects.Count - 1].transform.parent = levelParent.transform;
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
            canvasImage.GetComponent<Image>().color = Color.red;
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
                canvasImage.GetComponent<Image>().color = blueCol;
                delete = false;
            }
        }

        if(Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0.3f)
        {
            canvas.transform.position = cam.transform.position + cam.transform.forward * 23 + new Vector3(2, -2, 0);
            canvas.transform.LookAt(2 * canvas.transform.position - cam.transform.position);
        }

        
    if(Input.GetAxis("Oculus_GearVR_LThumbstickX") > 0.5)
    {
        levelParent.transform.Rotate(Vector3.up * (rotLightSpeed));
    }
    else if(Input.GetAxis("Oculus_GearVR_LThumbstickX") < -0.5)
    {
        levelParent.transform.Rotate(Vector3.up * (-rotLightSpeed));
    }

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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = cubeHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = cubeIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = dirLightHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = lightIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = characterHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = characterIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = checkPointHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = cubeHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = axisRotatorHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = rotatorIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = undoHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = redoHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = playHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = pastCubeHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = pastCubeIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = futureCubeHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = futureCubeIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = removeCharacterHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
            }

             //Rotate Light
            if(hit.transform.tag == "decreaseX") 
            {
                selectionText[15].GetComponent<Text>().color = Color.white;
            }
            else 
            {
                selectionText[15].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "decreaseX" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                directionalLight.transform.Rotate(Vector3.right * (-rotLightSpeed));
            }

            if(hit.transform.tag == "increaseX") 
            {
                selectionText[16].GetComponent<Text>().color = Color.white;
            }
            else 
            {
                selectionText[16].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "increaseX" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                directionalLight.transform.Rotate(Vector3.right * (rotLightSpeed));
            }

            if(hit.transform.tag == "decreaseY") 
            {
                selectionText[17].GetComponent<Text>().color = Color.white;
            }
            else 
            {
                selectionText[17].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "decreaseY" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                directionalLight.transform.Rotate(Vector3.up * (-rotLightSpeed));
            }

            if(hit.transform.tag == "increaseY") 
            {
                selectionText[18].GetComponent<Text>().color = Color.white;
            }
            else 
            {
                selectionText[18].GetComponent<Text>().color = Color.black;
            }
            if (hit.transform.tag == "increaseY" && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
            {
                directionalLight.transform.Rotate(Vector3.up * (rotLightSpeed));
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = loadHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = saveHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;

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

                //Cambia el texto de ayuda mostrado en el tooltip por el del boton al que se está apuntando
                tooltipText.GetComponent<Text>().text = stopHelp;
                //Cambia la imagen que se muestra en el tooltip del menu
                helpImg.GetComponent<Image>().sprite = defaultIMG;
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
