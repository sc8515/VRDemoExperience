using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControlScript : MonoBehaviour {


    public bool Active = false;
    OVRGrabbable grabbedinfo;

    // Use this for initialization
    void Start()
    {
        grabbedinfo = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbedinfo.isGrabbed)
        {
            if ((grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
            {
                Active = true;
            }
        }
    }
}
