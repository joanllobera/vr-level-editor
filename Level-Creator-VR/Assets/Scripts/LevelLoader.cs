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

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Save() 
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
        int index = 0;
        while (File.Exists(Application.dataPath + "/save" + index + ".txt")) 
        {
            index++;
        }
        
        File.WriteAllText(Application.dataPath + "/save" + index + ".txt", json);
    }

    public void Load()
    {
        //if (File.Exists(Application.dataPath + "/save.txt"))
        //{
        //    string json = File.ReadAllText(Application.dataPath + "/save.txt");
        //    data = JsonUtility.FromJson<LevelData>(json);

        //    // Lights
        //    foreach (Transform c in data.lights)
        //    {
        //        // We spawn the cubes
        //        GameObject g = (GameObject)Instantiate(Resources.Load("Light"));
        //        g.transform.SetPositionAndRotation(c.position, c.rotation);
        //        g.transform.localScale = c.localScale;
        //    }

        //    // Player
        //    foreach (Transform c in data.players)
        //    {
        //        // We spawn the cubes
        //        GameObject g = (GameObject)Instantiate(Resources.Load("Player"));
        //        g.transform.SetPositionAndRotation(c.position, c.rotation);
        //        g.transform.localScale = c.localScale;
        //    }

        //    // Cubes
        //    foreach (Transform c in data.cubes)
        //    {
        //        // We spawn the cubes
        //        GameObject g = (GameObject)Instantiate(Resources.Load("Cube"));
        //        g.transform.SetPositionAndRotation(c.position, c.rotation);
        //        g.transform.localScale = c.localScale;
        //    }
        //}
        //else
        //{
        //    Debug.Log("Could not load from file.");
        //}
    }
}
