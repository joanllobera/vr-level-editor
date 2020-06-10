using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] float speed;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    [SerializeField] Transform frontPoint;
    [SerializeField] Transform bottomPoint;
    [SerializeField] Transform backPoint;
    [SerializeField] bool isNpc;
    [SerializeField] GameObject Npc;
    private bool activateRaycasts = true;
    private bool canLeft = true, canRight = true, canFront = true, canBottom = true, canBack = true;
    [HideInInspector] public float zRot;
    public bool changePlane = false;
    public bool alive = false;
    private Vector3 initialPos;
    private Quaternion initialRot;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        mask = LayerMask.GetMask("Floor");
    }

    void Update()
    {
        if(alive)
        {
            Vector3 forward = (frontPoint.position - transform.position);
            Vector3 right = (rightPoint.position - transform.position);
            Vector3 left = (leftPoint.position - transform.position);
            Vector3 down = (bottomPoint.position - transform.position);
            Vector3 back = (backPoint.position - transform.position);
            DirectionalRaycasts(forward, right, left, down, back);
            transform.Translate(forward.normalized * Time.deltaTime * speed, Space.World);
        }
    }

    private void FixedUpdate()
    {
        
    }
    void DirectionalRaycasts(Vector3 forward, Vector3 right, Vector3 left, Vector3 down, Vector3 back)
    {
        RaycastHit hit;
        //forward
        if(canFront)
        {
            if (Physics.Raycast(transform.position, forward, out hit, forward.magnitude, mask))
            {
                if (!changePlane)
                {
                    Debug.Log("Forward hit");
                    Debug.DrawRay(transform.position, forward, Color.red);
                    transform.Rotate(0, -180, 0, Space.Self);
                    DeactivateRaycasts("front");
                }
                else
                {
                    changePlane = false;
                    Debug.Log("Forward hit change");
                    Debug.DrawRay(transform.position, forward, Color.red);
                    transform.Rotate(0, 0, 90, Space.Self);
                    DeactivateRaycasts("front");
                }
            }
            else
            {
                Debug.DrawRay(transform.position, forward, Color.white);
            }
        }
        
        //right
        if(canRight)
        {
            if (Physics.Raycast(transform.position, right, out hit, right.magnitude, mask))
            {

                Debug.Log("Right hit");
                Debug.DrawRay(transform.position, right, Color.red);
                Debug.Log(transform.rotation.eulerAngles.z);
                transform.Rotate(0, 90, 0, Space.Self);
                DeactivateRaycasts("right");
            }
            else
            {
                Debug.DrawRay(transform.position, right, Color.white);
            }
        }
        
        //left
        if(canLeft)
        {
            if (Physics.Raycast(transform.position, left, out hit, left.magnitude, mask))
            {
                Debug.Log("Left hit");
                Debug.DrawRay(transform.position, left, Color.red);
                transform.Rotate(0, -90, 0, Space.Self);
                DeactivateRaycasts("left");
            }
            else
            {
                Debug.DrawRay(transform.position, left, Color.white);
            }
        }

        //bottom
        if(canBottom)
        {
            if (Physics.Raycast(transform.position, down, out hit, down.magnitude, mask))
            {
                Debug.DrawRay(transform.position, down, Color.red);
            }
            else
            {
                Debug.Log("Bottom hit");
                Debug.DrawRay(transform.position, down, Color.white);
                if (!changePlane)
                {
                    transform.Rotate(0, -180, 0, Space.Self);
                    DeactivateRaycasts("bottom");
                }
                /*else if (changePlane)
                {
                    changePlane = false;
                    Debug.Log("Bottom hit change");
                    Debug.DrawRay(transform.position, down, Color.red);
                    transform.Rotate(0, 0, -90, Space.Self);
                    DeactivateRaycasts();
                }*/
            }
        }
        
        //back
        if(canBack)
        {
            if (Physics.Raycast(transform.position, back, out hit, back.magnitude, mask))
            {
                Debug.DrawRay(transform.position, back, Color.red);
            }
            else
            {
                Debug.Log("back hit");
                Debug.DrawRay(transform.position, back, Color.white);
            }
        }
    }

    void DeactivateRaycasts(string ray)
    {
        switch(ray)
        {
            case "front":
                canFront = false;
                Invoke("ActivateFront", 1f);
                break;
            case "left":
                canLeft = false;
                Invoke("ActivateLeft", 1f);
                break;
            case "right":
                canRight = false;
                Invoke("ActivateRight", 1f);
                break;
            case "bottom":
                canBottom = false;
                Invoke("ActivateBottom", 1f);
                break;
        }       
    }

    void ActivateRaycasts()
    {
        activateRaycasts = true;
    }

    void ActivateFront()
    {
        canFront = true;
    }
    void ActivateLeft()
    {
        canLeft = true;
    }
    void ActivateRight()
    {
        canRight = true;
    }
    void ActivateBottom()
    {
        canBottom = true;
    }

    public void ResetPos()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }

    public void Activate()
    {
        alive = true;
    }

    public void SetInitialValues(Transform initPos)
    {
        initialPos = initPos.position;
    }

    public void Deactivate()
    {
        alive = false;
    }
}
