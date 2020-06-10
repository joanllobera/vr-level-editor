using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour
{
    public GameObject canvasRef, mainMenuRef;

    public List<GameObject> uiElements = new List<GameObject>();
    public Sprite[] tutoList = new Sprite[5];
    //public GameObject tutoRefGO;
    public Image tutoRef;

    bool tutorialGoing = true;

    int currentTutorrial = 0;

    void setUI(bool state)
    {
        //tutoRef = tutoRefGO.GetComponent<Image>();
        /*foreach(GameObject canvasElement in uiElements)
        {
            canvasElement.SetActive(state);
            tutoRef.gameObject.SetActive(!state);
        }*/
        mainMenuRef.SetActive(state);
        tutoRef.gameObject.SetActive(!state);
    }

    public void restartTuto()
    {
        if(!tutorialGoing)
        {
            currentTutorrial = 0;
            tutoRef.sprite = tutoList[currentTutorrial];
            tutorialGoing = true;
            setUI(false);
        }
    }

    public void nextTutorial()
    {
        if(currentTutorrial < 4)
        {
            currentTutorrial++;
            tutoRef.sprite = tutoList[currentTutorrial];
        }
        else
        {
            setUI(true);
            tutorialGoing = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        /*foreach(Transform canvasElement in canvasRef.transform)
        {
            if(canvasElement.gameObject.name != "Background")
            {
                uiElements.Add(canvasElement.gameObject);
            }
            
            if(canvasElement.gameObject.name == "Remove")
            {
                break;
            }
        }*/
        setUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && tutorialGoing)
        {
            Debug.Log("Next Tutorial" + currentTutorrial);
            nextTutorial();            
        }
        if(Input.GetKeyDown(KeyCode.L) && !tutorialGoing)
        {
            Debug.Log("Restart Tutorial");
            restartTuto();
        }
    }
}