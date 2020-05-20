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
        if(activateRaycasts)
        {
            RaycastHit hit;
            //forward
            if (Physics.Raycast(transform.position, forward, out hit, forward.magnitude, mask))
            {
                if (!changePlane)
                {
                    Debug.Log("Forward hit");
                    Debug.DrawRay(transform.position, forward, Color.red);
                    transform.Rotate(0,-180,0, Space.Self);
                    DeactivateRaycasts();
                }
                else
                {
                    changePlane = false;
                    Debug.Log("Forward hit change");
                    Debug.DrawRay(transform.position, forward, Color.red);
                    transform.Rotate(0, 0, 90, Space.Self);
                    DeactivateRaycasts();
                }
            }
            else
            {
                Debug.DrawRay(transform.position, forward, Color.white);
            }
            //right
            if (Physics.Raycast(transform.position, right, out hit, right.magnitude, mask))
            {

                Debug.Log("Right hit");
                Debug.DrawRay(transform.position, right, Color.red);
                Debug.Log(transform.rotation.eulerAngles.z);
                transform.Rotate(0, 90, 0, Space.Self);
                DeactivateRaycasts();
            }
            else
            {
                Debug.DrawRay(transform.position, right, Color.white);
            }
            //left
            if (Physics.Raycast(transform.position, left, out hit, left.magnitude, mask))
            {
                Debug.Log("Left hit");
                Debug.DrawRay(transform.position, left, Color.red);
                transform.Rotate(0, -90, 0, Space.Self); 
                DeactivateRaycasts();
            }
            else
            {
                Debug.DrawRay(transform.position, left, Color.white);
            }
            //bottom
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
                    DeactivateRaycasts();
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
            //back
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

    void DeactivateRaycasts()
    {
        activateRaycasts = false;
        Invoke("ActivateRaycasts", 1f);
    }

    void ActivateRaycasts()
    {
        activateRaycasts = true;
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
}
