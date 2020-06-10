using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAll : MonoBehaviour
{
    [SerializeField]
    float speed;

    float rotationX;
    float rotationY;

    float incrementTowards;
    float incrementTowardsAux = 0;

    Camera cam1;

    GameObject quad, player;
    bool rotate, translate;

    void Start()
    {
        cam1 = GameObject.Find("CameraMovil").GetComponent<Camera>();
        //cam1.enabled = false;
        quad = GameObject.Find("Quad");
        //quad.SetActive(false);
        player = GameObject.Find("Player");
    }

    void swapCamera()
    {
        cam1.enabled = !cam1.enabled;
        if (cam1.enabled)
            quad.SetActive(true);

        else
            quad.SetActive(false);
    }

    void Update()
    {
        if (cam1.enabled)
        {
            //zoom in/out
            //if (OVRInput.Get(OVRInput.Button.PrimaryShoulder))
            if (Input.GetKey(KeyCode.G))
            {
                //Vector2 translation = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                //incrementTowards = translation.y;
                rotationY = Input.GetAxis("Mouse Y");
                incrementTowards = rotationY;

                if (incrementTowards != incrementTowardsAux)
                {

                    cam1.fieldOfView -= (incrementTowards * 2);
                    incrementTowardsAux = incrementTowards;
                }
            }
            else
            {
                //Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

                // rotationX = input.x;
                // rotationY = input.y;
                rotationX = Input.GetAxis("Mouse X");
                rotationY = Input.GetAxis("Mouse Y");

                transform.Rotate(rotationY * speed * Time.deltaTime, -rotationX * speed * Time.deltaTime, 0);
            }
        }
    }
}

