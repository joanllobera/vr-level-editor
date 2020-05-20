using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    [Serializable]
    public class LevelData 
    {
        public List<Transform> players = new List<Transform>();
        public List<Transform> lights = new List<Transform>();
        public List<Transform> cubes = new List<Transform>();
    }
    LevelData data = new LevelData();

    GameObject leftHand;
    GameObject loadCanvas, saveCanvas;
    GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        loadCanvas = GameObject.FindGameObjectWithTag("loadCanvas");
        saveCanvas = GameObject.FindGameObjectWithTag("saveCanvas");
        mainMenu = GameObject.Find("MainMenu");
        leftHand = GameObject.Find("LeftControllerAnchor");
        loadCanvas.SetActive(false);
        saveCanvas.SetActive(false);
    }

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(leftHand.transform.position, leftHand.transform.forward, out hit, 30))
        {
            if (hit.transform.tag == "backButton") 
            {
                Back();
            }
            if(hit.transform.tag == "saveSlot") 
            {
                SaveSlot ss = hit.transform.gameObject.GetComponent<SaveSlot>();
                Save(ss.slotNum);
            }
            if(hit.transform.tag == "loadSlot") 
            {
                LoadSlot ls = hit.transform.gameObject.GetComponent<LoadSlot>();
                LoadSave(Application.dataPath + "/save" + ls.slotNum + ".txt");
            }
        }
    }

    public void Save(int n) 
    {
        // We save the cube's transform
        data.cubes.Clear();
        data.players.Clear();
        data.lights.Clear();

        // Cubes
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");
        foreach(GameObject c in cubes)
        {
            data.cubes.Add(c.transform);
        }

        // Lights
        GameObject[] lights = GameObject.FindGameObjectsWithTag("light");
        foreach (GameObject l in lights)
        {
            data.lights.Add(l.transform);
        }

        // Players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            data.players.Add(p.transform);
        }

        // We save the data in the json
        string json = JsonUtility.ToJson(data);        
        File.WriteAllText(Application.dataPath + "/save" + n + ".txt", json);
    }

    public void OpenLoadCanvas()
    {
        loadCanvas.SetActive(true);
        mainMenu.SetActive(false);

        //// We load the saves' names.
        //string[] files = System.IO.Directory.GetFiles(Application.dataPath, "*.txt");
    }

    public void OpenSaveCanvas() 
    {
        saveCanvas.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Back() 
    {
        loadCanvas.SetActive(false);
        saveCanvas.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadSave(string name) 
    {
        if (File.Exists(name))
        {
            string json = File.ReadAllText(name);
            data = JsonUtility.FromJson<LevelData>(json);

            // Lights
            foreach (Transform c in data.lights)
            {
                // We spawn the cubes
                GameObject g = (GameObject)Instantiate(Resources.Load("Light"));
                g.transform.SetPositionAndRotation(c.position, c.rotation);
                g.transform.localScale = c.localScale;
            }

            // Player
            foreach (Transform c in data.players)
            {
                // We spawn the cubes
                GameObject g = (GameObject)Instantiate(Resources.Load("Player"));
                g.transform.SetPositionAndRotation(c.position, c.rotation);
                g.transform.localScale = c.localScale;
            }

            // Cubes
            foreach (Transform c in data.cubes)
            {
                // We spawn the cubes
                GameObject g = (GameObject)Instantiate(Resources.Load("Cube"));
                g.transform.SetPositionAndRotation(c.position, c.rotation);
                g.transform.localScale = c.localScale;
            }
        }
        else
        {
            Debug.Log("Could not load from file.");
        }
    }
}
