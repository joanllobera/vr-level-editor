using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Hand_Behaviour : MonoBehaviour
{
    [SerializeField] Transform handParent;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Round(handParent.position.x),
                                         Mathf.Round(handParent.position.y),
                                         Mathf.Round(handParent.position.z));

        //transform.rotation = Quaternion.identity;
    }
}
