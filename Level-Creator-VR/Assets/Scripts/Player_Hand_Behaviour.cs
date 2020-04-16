using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hand_Behaviour : MonoBehaviour
{
    enum Direction {Up, Down, Right, Left, Forward, Back};

    public Material unplaceableMat;
    public Material placeableMat;
    struct RaycastedObject
    {
        public Transform transform;
        public Direction direction;

        public RaycastedObject(Transform _transform, Direction _direction)
        {
            transform = _transform;
            direction = _direction;
        }
    }
    [SerializeField] Transform handParent;
    private List<RaycastedObject> raycastedObjects;

    //hit for distance checking
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        //new Vector3(Mathf.Round(handParent.position.x),
                                         //Mathf.Round(handParent.position.y),
                                         //Mathf.Round(handParent.position.z));

        //transform.rotation = Quaternion.identity;

        raycastedObjects = new List<RaycastedObject>();

        //Raycast up
        if(Physics.Raycast(transform.position, Vector3.up, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Up);
                raycastedObjects.Add(obj);
            }
        }
        //Raycast down
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Down);
                raycastedObjects.Add(obj);
            }
        }   
        //Raycast Right
        if(Physics.Raycast(transform.position, Vector3.right, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Right);
                raycastedObjects.Add(obj);
            }
        }  
        //Raycast Left
        if(Physics.Raycast(transform.position, Vector3.left, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Left);
                raycastedObjects.Add(obj);
            }
        }  
        //Raycast forward
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Forward);
                raycastedObjects.Add(obj);
            }
        }  
        //Raycast backwards
        if(Physics.Raycast(transform.position, Vector3.back, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Back);
                raycastedObjects.Add(obj);
            }
        }  

        float minDist = Mathf.Infinity;
        RaycastedObject closestObject = new RaycastedObject(null, Direction.Up);

        foreach(RaycastedObject obj in raycastedObjects)
        {
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            if(dist < minDist)
            {
                closestObject = obj;
                minDist = dist;
            }
        }

        if(raycastedObjects.Count > 0)
        {
            float x = 0;
            float y = 0;
            float z = 0;
            switch(closestObject.direction)
            {
                case Direction.Up:
                    x = closestObject.transform.position.x;
                    y = closestObject.transform.position.y - closestObject.transform.localScale.y;
                    z = closestObject.transform.position.z;
                    break;
                case Direction.Down:
                    x = closestObject.transform.position.x;
                    y = closestObject.transform.position.y + closestObject.transform.localScale.y;
                    z = closestObject.transform.position.z;
                    break;
                case Direction.Left:
                    x = closestObject.transform.position.x + closestObject.transform.localScale.x;
                    y = closestObject.transform.position.y;
                    z = closestObject.transform.position.z;
                    break;
                case Direction.Right:
                    x = closestObject.transform.position.x - closestObject.transform.localScale.x;
                    y = closestObject.transform.position.y;
                    z = closestObject.transform.position.z;
                    break;
                case Direction.Forward:
                    x = closestObject.transform.position.x;
                    y = closestObject.transform.position.y;
                    z = closestObject.transform.position.z - closestObject.transform.localScale.z;
                    break;
                case Direction.Back:
                    x = closestObject.transform.position.x;
                    y = closestObject.transform.position.y;
                    z = closestObject.transform.position.z + closestObject.transform.localScale.z;
                    break;
            }
            this.gameObject.GetComponent<MeshRenderer>().material = placeableMat;
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            transform.position = handParent.transform.position;
            this.gameObject.GetComponent<MeshRenderer>().material = unplaceableMat;
        }

    }
}
