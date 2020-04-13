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
        public Transform player;
        public Transform light;
        public List<Transform> cubes = new List<Transform>();
    }
    LevelData data = new LevelData();

    // Start is called before the first frame update
    void Start()
    {
        data.player = GameObject.Find("Player").transform;
        data.light = GameObject.Find("Light").transform;
    }

    public void Save() 
    {
        // We save the cube's transform
        data.cubes.Clear();
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");
        foreach(GameObject c in cubes)
        {
            data.cubes.Add(c.transform);
        }

        // We save the data in the json
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string json = File.ReadAllText(Application.dataPath + "/save.txt");
            data = JsonUtility.FromJson<LevelData>(json);

            GameObject player = GameObject.Find("Player");
            player.transform.SetPositionAndRotation(data.player.position, data.player.rotation);
            player.transform.localScale = data.player.localScale;

            GameObject light = GameObject.Find("Light");
            light.transform.SetPositionAndRotation(data.light.position, data.light.rotation);
            light.transform.localScale = data.player.localScale;

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
