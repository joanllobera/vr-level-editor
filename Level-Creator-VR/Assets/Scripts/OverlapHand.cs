using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapHand : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("activateCube"))
        {
            spawner.cube = true;
            spawner.light = false;
            spawner.player = false;
            spawner.checkpoint = false;
            spawner.finish = false;
            spawner.axisRotator = false;
        }
        if(other.transform.CompareTag("activateLight"))
        {
            spawner.cube = false;
            spawner.light = true;
            spawner.player = false;
            spawner.checkpoint = false;
            spawner.finish = false;
            spawner.axisRotator = false;
        }
        if(other.transform.CompareTag("activatePlayer"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.checkpoint = false;
            spawner.player = true;
            spawner.axisRotator = false;
            spawner.finish = false;
        }
        if(other.transform.CompareTag("activateCheckpoint"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.player = false;
            spawner.checkpoint = true;
            spawner.finish = false;
            spawner.axisRotator = false;
        }
        if(other.transform.CompareTag("activateFinish"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.player = false;
            spawner.checkpoint = false;
            spawner.finish = true;
            spawner.axisRotator = false;
        }
        if(other.transform.CompareTag("activateAxisRotator"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.player = false;
            spawner.checkpoint = false;
            spawner.finish = false;
            spawner.axisRotator = true;
        }
        if(other.transform.CompareTag("undo"))
        {
            spawner.urManager.Undo();
        }
        if(other.transform.CompareTag("play"))
        {
            
        }
        if(other.transform.CompareTag("load"))
        {
            spawner.levelLoader.OpenLoadCanvas();
        }
        if(other.transform.CompareTag("save"))
        {
            spawner.gualdal = true;
            spawner.levelLoader.OpenSaveCanvas();
        }
    }   
}
