using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitTrigger : MonoBehaviour {

    OVRGrabbable grabInfo; // Calls grabbable in order to use grabInfo, grabbedBy & m_controller. This is currently empty and will be assigned later on. (Line 25)
    public Transform coreTransform;
    Transform orbitTransform;

	// Use this for initialization
	void Start () {
        orbitTransform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        orbitTransform.position = coreTransform.position; // I make sure the orbit trigger is always centered around the MenuCore. 
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "UI") // This is checking that the elements comming in are UI tagged elements. 
        {
            grabInfo = col.GetComponent<OVRGrabbable>(); // Grabs the grabbable from the object triggering the response. 
            // Vibration Actuation on the controller that is grabbing the object triggering the response.
            VibrationManager.singleton.TriggerVibration(20, 2, 255, grabInfo.grabbedBy.m_controller);
            // I change the tag to ActiveUI because the object is inside the trigger sphere. Therefore, the orbital movement will be activated. 
            col.tag = "ActiveUI";
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "ActiveUI")  // This is checking that the elements comming out are ActiveUI tagged elements.
        {
            grabInfo = col.GetComponent<OVRGrabbable>();
            if (grabInfo.isGrabbed)
            {
                VibrationManager.singleton.TriggerVibration(20, 2, 255, grabInfo.grabbedBy.m_controller);
                col.tag = "UI"; // I change the tag to UI because the object is outside the trigger sphere. 
                if (col.GetComponent<Renderer>().enabled == false)
                {
                    // I make sure that the renderer component of the object leaving the trigger is activated.
                    col.GetComponent<Renderer>().enabled = true; 
                }
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "UI") // This is checking if the elements in are UI tagged elements.
        {
            // I change the tag to ActiveUI because the object is inside the trigger sphere. Therefore, the orbital movement will be activated. 
            col.tag = "ActiveUI"; 
        }
    }
}
