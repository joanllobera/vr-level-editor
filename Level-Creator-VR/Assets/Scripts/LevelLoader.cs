using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    [Serializable]
    public class SaveObject 
    {
        public Vector3 pos, scale;
        public Quaternion rot;
        public SaveObject(Vector3 p, Quaternion r, Vector3 s) 
        {
            pos = p; scale = s; rot = r;
        }
    }

    [Serializable]
    public class LevelData 
    {
        public List<SaveObject> players = new List<SaveObject>();
        public List<SaveObject> lights = new List<SaveObject>();
        public List<SaveObject> cubes = new List<SaveObject>();
        public List<SaveObject> checkpoints = new List<SaveObject>();
        public List<SaveObject> goals = new List<SaveObject>();
        public List<SaveObject> rotators = new List<SaveObject>();
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
        Save(1);
        LoadSave(Application.dataPath + "/save1.txt");
    }

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(leftHand.transform.position, leftHand.transform.forward, out hit, 30) && Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.3f)
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
        data.checkpoints.Clear();
        data.goals.Clear();
        data.rotators.Clear();

        // Cubes
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");
        foreach(GameObject c in cubes)
        {
            data.cubes.Add(new SaveObject(c.transform.position, c.transform.rotation, c.transform.localScale));
        }

        // Lights
        GameObject[] lights = GameObject.FindGameObjectsWithTag("light");
        foreach (GameObject l in lights)
        {
            data.lights.Add(new SaveObject(l.transform.position, l.transform.rotation, l.transform.localScale));
        }

        // Players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            data.players.Add(new SaveObject(p.transform.position, p.transform.rotation, p.transform.localScale));
        }

        // We save the data in the json
        string json = JsonUtility.ToJson(data);        
        File.WriteAllText(Application.dataPath + "/save" + n + ".txt", json);
    }

    public void OpenLoadCanvas()
    {
        loadCanvas.SetActive(true);
        mainMenu.SetActive(false);
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
            foreach (SaveObject c in data.lights)
            {
                // We spawn the cubes
                GameObject g = (GameObject)Instantiate(Resources.Load("Light"));
                g.transform.SetPositionAndRotation(c.pos, c.rot);
                g.transform.localScale = c.scale;
                g.SetActive(true);
            }

            // Player
            foreach (SaveObject c in data.players)
            {
                // We spawn the cubes
                GameObject g = (GameObject)Instantiate(Resources.Load("testCharacter"));
                g.transform.SetPositionAndRotation(c.pos, c.rot);
                g.transform.localScale = c.scale;
                g.SetActive(true);
            }

            // Cubes
            foreach (SaveObject c in data.cubes)
            {
                // We spawn the cubes
                GameObject g = (GameObject)Instantiate(Resources.Load("Cube_Spawned"));
                g.transform.SetPositionAndRotation(c.pos, c.rot);
                g.transform.localScale = c.scale;
                g.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Could not load from file.");
        }
    }
}
