using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{
    [SerializeField] Transform lightSource;
    [SerializeField] bool worksOnlyInShadow;
    //[SerializeField] Transform cameraAR;
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 pointC;
    private Vector3 dirA;
    private Vector3 dirB;
    private Vector3 dirC;
    private Vector3 cameraPos;
    private Mesh mesh;
    private BoxCollider collider;
    private bool collideA = false;
    private bool collideB = false;
    private bool collideC = false;
    int layerMask = 1 << 0;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        collider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        //Compute ray directions
        cameraPos = lightSource.position;

        Bounds bounds = mesh.bounds;
        pointA = transform.TransformPoint(bounds.size.x/2, 0, 0);
        pointB = transform.TransformPoint(-bounds.size.x / 2, 0,0);
        pointC = transform.TransformPoint(0, 0, 0);
        dirA = cameraPos - pointA;
        dirB = cameraPos - pointB;
        dirC = cameraPos - pointC;
        Debug.DrawRay(pointA, dirA, Color.green);
        Debug.DrawRay(pointB, dirB, Color.green);
        Debug.DrawRay(pointC, dirC, Color.green);

        //Raycasting to check if its overlapped by another object
        RaycastHit hit;
        if (Physics.Raycast(pointA, dirA, out hit, dirA.magnitude))
        {
            Debug.DrawRay(pointA, dirA, Color.red);
            collideA = true;
        }
        else
        {
            collideA = false;
        }
        if (Physics.Raycast(pointB, dirB, out hit, dirB.magnitude))
        {
            Debug.DrawRay(pointB, dirB, Color.red);
            collideB = true;
        }
        else
        {
            collideB = false;
        }
        if (Physics.Raycast(pointC, dirC, out hit, dirC.magnitude))
        {
            Debug.DrawRay(pointC, dirC, Color.red);            
            collideC = true;
        }
        else
        {
            collideC = false;
        }

        if(collideA && collideB && collideC)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
        //check if only works in shadow instead of sun
        if (!worksOnlyInShadow)
        {
            collider.enabled = !collider.enabled;
        }
    }
}
