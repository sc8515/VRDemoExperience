using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlZScript : MonoBehaviour{

    public bool ZActive = false;
    OVRGrabbable grabbedinfoZ;

    // Use this for initialization
    void Start()
    {
        grabbedinfoZ = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbedinfoZ.isGrabbed)
        {
            if ((grabbedinfoZ.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfoZ.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
            {
                ZActive = true;
            }
        }
    }
}
