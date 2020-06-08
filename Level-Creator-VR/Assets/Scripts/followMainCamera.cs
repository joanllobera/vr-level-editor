using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMainCamera : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.Find("GameController");        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + transform.forward * 15 + new Vector3(2,-2,0);
    }
}
