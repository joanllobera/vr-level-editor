using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private bool spawnOnce;
    public List<GameObject> spawnedObjects;
    public GameObject cubePrefab;
    public GameObject leftHand;
    public GameObject lightPrefab;
    public GameObject playerPrefab;
    public GameObject cubeHand, lightHand, playerHand;
    public GameObject testObject;
    public List<GameObject> selectionText = new List<GameObject>();

    private bool cube, light, player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cube)
        {
            cubeHand.GetComponent<Renderer>().enabled = true;
            lightHand.GetComponent<Renderer>().enabled = false;
            playerHand.GetComponent<Renderer>().enabled = false;
        }
        else if (light)
        {
            cubeHand.GetComponent<Renderer>().enabled = false;
            lightHand.GetComponent<Renderer>().enabled = true;
            playerHand.GetComponent<Renderer>().enabled = false;
        }
        else if (player)
        {
            cubeHand.GetComponent<Renderer>().enabled = false;
            lightHand.GetComponent<Renderer>().enabled = false;
            playerHand.GetComponent<Renderer>().enabled = true;
        }


        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.3f && !spawnOnce)
        {
            spawnOnce = true;

            if(cube)
            {
                spawnedObjects.Add(Instantiate(cubePrefab, cubeHand.transform.position, cubeHand.transform.rotation));
                spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano
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
                player = true;
            }

            if (hit.transform.tag == "undo")
            {
                selectionText[3].GetComponent<Text>().color = Color.white;
            }
            else
            {
                selectionText[3].GetComponent<Text>().color = Color.black;
            }
        }
        
    }
}
