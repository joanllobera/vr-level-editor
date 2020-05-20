using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    void Start()
    {
       // levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Level_Manager>();
       // musicManager = MusicManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //levelManager.npcCount++;
        }
    }

}
