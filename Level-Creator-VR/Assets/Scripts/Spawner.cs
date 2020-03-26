using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool spawnOnce;
    public List<GameObject> spawnedObjects;
    public GameObject cube;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.3f && !spawnOnce)
        {
            spawnOnce = true;
            spawnedObjects.Add(Instantiate(cube, cube.transform.position, cube.transform.rotation));
            spawnedObjects[spawnedObjects.Count - 1].GetComponent<Cube_Hand_Behaviour>().enabled = false; //desactivo el script para que no siga a la mano

        }
        else
        {
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") < 0.3f)
                spawnOnce = false;
        }
           
    }
}
