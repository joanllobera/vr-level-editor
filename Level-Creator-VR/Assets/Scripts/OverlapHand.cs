using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        GameObject parent = GameObject.Find("hands:b_r_index_ignore");
        if(parent != null)
        {
            transform.parent = parent.transform;
            transform.localPosition = new Vector3(0.01f, 0.01f, -0.04f);
        }
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
            spawner.selectionText[0].GetComponent<Text>().color = Color.white;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;

        }
        if(other.transform.CompareTag("activateLight"))
        {
            spawner.cube = false;
            spawner.light = true;
            spawner.player = false;
            spawner.checkpoint = false;
            spawner.finish = false;
            spawner.axisRotator = false;
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.white;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
        }
        if(other.transform.CompareTag("activatePlayer"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.checkpoint = false;
            spawner.player = true;
            spawner.axisRotator = false;
            spawner.finish = false;
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.white;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
        }
        if(other.transform.CompareTag("activateCheckpoint"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.player = false;
            spawner.checkpoint = true;
            spawner.finish = false;
            spawner.axisRotator = false;
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.white;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
            
        }
        if(other.transform.CompareTag("activateFinish"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.player = false;
            spawner.checkpoint = false;
            spawner.finish = true;
            spawner.axisRotator = false;
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.white;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
        }
        if(other.transform.CompareTag("activateAxisRotator"))
        {
            spawner.cube = false;
            spawner.light = false;
            spawner.player = false;
            spawner.checkpoint = false;
            spawner.finish = false;
            spawner.axisRotator = true;
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.white;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
        }
        if(other.transform.CompareTag("undo"))
        {
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.white;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
            spawner.urManager.Undo();
        }
        if(other.transform.CompareTag("redo"))
        {
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.white;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
            spawner.urManager.Redo();
        }
        if(other.transform.CompareTag("play"))
        {
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.white;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
            //PLAY
        }
        if(other.transform.CompareTag("load"))
        {
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.black;
            spawner.selectionText[10].GetComponent<Text>().color = Color.white;
            spawner.levelLoader.OpenLoadCanvas();
        }
        if(other.transform.CompareTag("save"))
        {
            spawner.selectionText[0].GetComponent<Text>().color = Color.black;
            spawner.selectionText[1].GetComponent<Text>().color = Color.black;
            spawner.selectionText[2].GetComponent<Text>().color = Color.black;
            spawner.selectionText[3].GetComponent<Text>().color = Color.black;
            spawner.selectionText[4].GetComponent<Text>().color = Color.black;
            spawner.selectionText[5].GetComponent<Text>().color = Color.black;
            spawner.selectionText[6].GetComponent<Text>().color = Color.black;
            spawner.selectionText[7].GetComponent<Text>().color = Color.black;
            spawner.selectionText[8].GetComponent<Text>().color = Color.black;
            spawner.selectionText[9].GetComponent<Text>().color = Color.white;
            spawner.selectionText[10].GetComponent<Text>().color = Color.black;
            spawner.gualdal = true;
            spawner.levelLoader.OpenSaveCanvas();
        }
    }   
}
