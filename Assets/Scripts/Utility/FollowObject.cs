using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject followTarget;
    public float OffsetY = 0;
    

    // Update is called once per frame
    void LateUpdate()
    {
        if(followTarget != null)
        {
            transform.position = followTarget.transform.position + new Vector3(0, OffsetY, 0);
        }
        else
        {
            Debug.Log(name + " has no target to follow");
        }
    }
}
