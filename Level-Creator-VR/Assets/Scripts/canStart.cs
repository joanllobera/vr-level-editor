using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class canStart : MonoBehaviour
{
    float timer = 3f;

    void startButton()
    {
        if (GameObject.Find("Player") != null && GameObject.Find("Goal") != null && GameObject.Find("Light") != null)
        {
            //play function
        }
        else
        {
            timer = 3f;
            GameObject.Find("Text").GetComponent<Text>().text = "Something missing";
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer<=0)
        {
            GameObject.Find("Text").GetComponent<Text>().text = "";
        }

        //////provisional
        if(Input.GetKeyDown(KeyCode.V))
        {
            startButton();
        }
    }
}
