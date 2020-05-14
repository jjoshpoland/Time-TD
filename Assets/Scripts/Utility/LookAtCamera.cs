using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera cameraToLookAt;
    public bool lookAtMain;

    private void Start()
    {
        if(lookAtMain)
        {
            cameraToLookAt = Camera.main;
        }
    }

    void Update()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
        transform.rotation = cameraToLookAt.transform.rotation;

    }
}
