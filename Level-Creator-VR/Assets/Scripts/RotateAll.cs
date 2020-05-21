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

    Camera cam1, cam2;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("OVRCameraRig_LevelCreator");
        //cam1 = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
        cam2 = GameObject.Find("CameraMovil").GetComponent<Camera>();

        //cam1.enabled = true;
        cam2.enabled = false;
    }

    void Update()
    {
        //no se quins botons son del mando VR, falta configurar a botons correctes
        if (Input.GetKeyDown(KeyCode.C))
        //if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            // cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;

            if(cam2.enabled)
                player.SetActive(false);

            else
                player.SetActive(true);

        }

        if (cam2.enabled)
        {

            //Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

           // rotationX = input.x;
            // = input.y;

            rotationX= Input.GetAxis("Mouse X");
            rotationY =Input.GetAxis("Mouse Y");

            transform.Rotate(rotationY * speed * Time.deltaTime, -rotationX * speed * Time.deltaTime, 0);
            
            //zoom in/out
            /*if (OVRInput.Get(OVRInput.Button.PrimaryShoulder))
            {
                Vector2 translation = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

                incrementTowards = translation.y;

                if (incrementTowards != incrementTowardsAux)
                {
                    
                    cam2.fieldOfView -= (incrementTowards * 2);
                    incrementTowardsAux = incrementTowards;
                }
            }*/
        }
    }
}
