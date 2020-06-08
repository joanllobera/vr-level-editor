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
    public Quaternion rotation;

    private void Start()
    {
        unplaceableMat = (Material)Resources.Load("unplaceableMat", typeof(Material));
        placeableMat = (Material)Resources.Load("placeableMat", typeof(Material));
        handParent = GameObject.Find("Raycaster").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f))
        {
            if(hit.transform.tag == "cube")
            {
                rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 1.5f))
        {
            if(hit.transform.tag == "cube")
            {
                rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1.5f))
        {
            if(hit.transform.tag == "cube")
            {
                rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.5f))
        {
            if(hit.transform.tag == "cube")
            {
                rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1.5f))
        {
            if(hit.transform.tag == "cube")
            {
                rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1.5f))
        {
            if(hit.transform.tag == "cube")
            {
                rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
        }
        /*transform.position = new Vector3(Mathf.Round(handParent.position.x),
                                         Mathf.Round(handParent.position.y),
                                         Mathf.Round(handParent.position.z));

        transform.rotation = Quaternion.identity;*/

        raycastedObjects = new List<RaycastedObject>();
        Debug.DrawRay(handParent.position, handParent.TransformDirection(Vector3.up) * 2, Color.red);
        Debug.DrawRay(handParent.position, handParent.TransformDirection(Vector3.down) * 2, Color.red);
        Debug.DrawRay(handParent.position, handParent.TransformDirection(Vector3.right) * 2, Color.red);
        Debug.DrawRay(handParent.position, handParent.TransformDirection(Vector3.left) * 2, Color.red);
        Debug.DrawRay(handParent.position, handParent.TransformDirection(Vector3.forward) * 2, Color.red);
        Debug.DrawRay(handParent.position, handParent.TransformDirection(Vector3.back) * 2, Color.red);

        //Raycast up
        if(Physics.Raycast(handParent.position, handParent.TransformDirection(Vector3.up) * 2.5f, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Up);
                raycastedObjects.Add(obj);
            }
        }
        //Raycast down
        if(Physics.Raycast(handParent.position, handParent.TransformDirection(Vector3.down) * 2.5f, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Down);
                raycastedObjects.Add(obj);
            }
        }   
        //Raycast Right
        if(Physics.Raycast(handParent.position, handParent.TransformDirection(Vector3.right) * 2.5f, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Right);
                raycastedObjects.Add(obj);
            }
        }  
        //Raycast Left
        if(Physics.Raycast(handParent.position, handParent.TransformDirection(Vector3.left) * 2.5f, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Left);
                raycastedObjects.Add(obj);
            }
        }  
        //Raycast forward
        if(Physics.Raycast(handParent.position, handParent.TransformDirection(Vector3.forward) * 2.5f, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Forward);
                raycastedObjects.Add(obj);
            }
        }  
        //Raycast backwards
        if(Physics.Raycast(transform.position, handParent.TransformDirection(Vector3.back) * 2.5f, out hit, 2f))
        {
            if(hit.transform.tag == "cube")
            {
                RaycastedObject obj = new RaycastedObject(hit.transform, Direction.Back);
                raycastedObjects.Add(obj);
            }
        }  

        

        if(raycastedObjects.Count > 0)
        {
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
            this.gameObject.GetComponentInChildren<MeshRenderer>().material = placeableMat;
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            //transform.position = handParent.transform.position;
            this.gameObject.GetComponentInChildren<MeshRenderer>().material = unplaceableMat;
        }

    }
}
