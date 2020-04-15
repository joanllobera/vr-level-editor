using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hand_Behaviour : MonoBehaviour
{
    [SerializeField] Transform handParent;
    private List<Transform> raycastedObjects;

    //hit for distance checking
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        transform.position = handParent.transform.position;//new Vector3(Mathf.Round(handParent.position.x),
                                         //Mathf.Round(handParent.position.y),
                                         //Mathf.Round(handParent.position.z));

        //transform.rotation = Quaternion.identity;

        raycastedObjects = new List<Transform>();

        //Raycast up
        if(Physics.Raycast(transform.position, Vector3.up, out hit, 2f))
        {
            if(hit.transform.tag == "Floor")
            {
                raycastedObjects.Add(hit.transform);
            }
        }
        //Raycast down
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if(hit.transform.tag == "Floor")
            {
                raycastedObjects.Add(hit.transform);
            }
        }   
        //Raycast Right
        if(Physics.Raycast(transform.position, Vector3.right, out hit, 2f))
        {
            if(hit.transform.tag == "Floor")
            {
                raycastedObjects.Add(hit.transform);
            }
        }  
        //Raycast Left
        if(Physics.Raycast(transform.position, Vector3.left, out hit, 2f))
        {
            if(hit.transform.tag == "Floor")
            {
                raycastedObjects.Add(hit.transform);
            }
        }  
        //Raycast forward
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, 2f))
        {
            if(hit.transform.tag == "Floor")
            {
                raycastedObjects.Add(hit.transform);
            }
        }  
        //Raycast backwards
        if(Physics.Raycast(transform.position, Vector3.back, out hit, 2f))
        {
            if(hit.transform.tag == "Floor")
            {
                raycastedObjects.Add(hit.transform);
            }
        }  

        foreach(Transform t in raycastedObjects)
        {

        }

    }
}
