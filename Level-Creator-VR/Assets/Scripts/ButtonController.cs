using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor(string tag)
    {
        if(tag == gameObject.tag)
        {
            isPressed = true;
        }
        else{
            isPressed = false;
        }

        if(!isPressed){
            GetComponentInChildren<Image>().color = Color.blue;
        }
        else if(isPressed){
            GetComponentInChildren<Image>().color = Color.yellow;
        }

        
    }
}
