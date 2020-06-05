using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BifurcationAvoider : MonoBehaviour
{
    public byte raycastCount;
    public bool isCorner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        raycastCount = 0;
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f))
        {
            if(hit.transform.tag == "cube")
            {
                raycastCount++;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1f))
        {
            if(hit.transform.tag == "cube")
            {
                raycastCount++;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1f))
        {
            if(hit.transform.tag == "cube")
            {
                raycastCount++;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f))
        {
            if(hit.transform.tag == "cube")
            {
                raycastCount++;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1f))
        {
            if(hit.transform.tag == "cube")
            {
                raycastCount++;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1f))
        {
            if(hit.transform.tag == "cube")
            {
                raycastCount++;
            }
        }


        isCorner = false;
        if(raycastCount > 1)
        {
            isCorner = true;
        }
    }
}
