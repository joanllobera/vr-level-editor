using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSun : MonoBehaviour
{
    GameObject sun;
    private bool underSun = false;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        sun = GameObject.FindWithTag("Sun");
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = mesh.bounds;
        Vector3 sunDir = sun.transform.forward;
        sunDir.Normalize();
        sunDir *= 100f;
        RaycastHit hit;

        if (!Physics.Raycast(transform.TransformPoint(0, bounds.size.y /2f, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, out hit, 30))
        {
            Debug.DrawLine(transform.TransformPoint(0, bounds.size.y / 2, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.TransformPoint(0, bounds.size.y / 2, 0), transform.TransformPoint(0, bounds.size.y / 2, 0) - sunDir, Color.green);
        }
    }
}
