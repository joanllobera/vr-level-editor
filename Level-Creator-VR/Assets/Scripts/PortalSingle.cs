using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSingle : MonoBehaviour
{
    public GameObject targetPortal;
    public Vector3 targetRotation;
    public bool isObjective;
    public bool isHit;
    private GameObject camera;
    private Vector3 cameraPos;
    private LayerMask mask;
    private bool onCooldown = false;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        mask = LayerMask.GetMask("Portals");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isObjective)
        {
            cameraPos = camera.transform.position;
            Vector3 dir = cameraPos - transform.position;
            RaycastHit hit;
            Debug.DrawRay(transform.position, dir, Color.green);
            if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude, mask))
            {
                Debug.DrawRay(transform.position, dir, Color.red);
                if (hit.transform.gameObject == targetPortal)
                {
                    active = true;
                    targetPortal.GetComponent<PortalSingle>().Activate(true);
                }

            }
            else
            {
                active = false;
                targetPortal.GetComponent<PortalSingle>().Activate(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (active && !onCooldown)
            {
                Vector3 targetPos = targetPortal.transform.position;
                targetPos.y = other.gameObject.transform.position.y;
                other.gameObject.transform.Rotate(targetRotation, Space.Self);
                other.gameObject.transform.position = targetPos;
                RaycastHit hit;
                if (Physics.Raycast(targetPortal.transform.position, -other.gameObject.transform.up, out hit, 20f, LayerMask.GetMask("Floor")))
                {
                    targetPos.y = hit.point.y + (other.gameObject.transform.localScale.y * other.gameObject.transform.up.y);
                    other.gameObject.transform.position = targetPos;
                }
                PortalCooldown();
                targetPortal.GetComponent<PortalSingle>().PortalCooldown();
            }
        }
    }

    public void PortalCooldown()
    {
        onCooldown = true;
        Invoke("ResetPortal", 1.5f);
    }

    private void ResetPortal()
    {
        onCooldown = false;
    }

    public void Activate(bool activate)
    {
        active = activate;
    }
}
