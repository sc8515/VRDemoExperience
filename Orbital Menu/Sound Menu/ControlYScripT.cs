using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlYScript : MonoBehaviour{

    public bool YActive = false;
    OVRGrabbable grabbedinfoY;

    // Use this for initialization
    void Start()
    {
        grabbedinfoY = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbedinfoY.isGrabbed)
        {
            if ((grabbedinfoY.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfoY.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
            {
                YActive = true;
            }
        }
    }
}
