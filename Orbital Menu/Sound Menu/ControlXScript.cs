using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlXScript : MonoBehaviour {


    public bool XActive = false;
    OVRGrabbable grabbedinfoX;

    // Use this for initialization
    void Start () {
        grabbedinfoX = GetComponent<OVRGrabbable>();
    }
	
	// Update is called once per frame
	void Update () {
        if (grabbedinfoX.isGrabbed)
        {
            if ((grabbedinfoX.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfoX.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
            {
                XActive = true;
            }
        }
    }
}
