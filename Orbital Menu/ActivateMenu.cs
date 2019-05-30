using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenu : MonoBehaviour
{

    bool MenuActivated = true;
    bool LookActive = false;
    OVRGrabbable grabbedinfo;
    LookAtInteraction lookatInteraction;

    public ParticleSystem fireflies;
    // Use this for initialization
    void Start()
    {
        grabbedinfo = GetComponent<OVRGrabbable>();
        lookatInteraction = GameObject.Find("RaycastCone").GetComponent<LookAtInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LookActive == false) 
        {
            if (grabbedinfo.isGrabbed)
            {
                if ((grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
                {
                    if (MenuActivated)
                    {
                        print("MenuDeactivate!");
                        MenuDeactivation();
                    }
                    else if (MenuActivated == false)
                    {
                        print("MenuActivate!");
                        MenuActivation();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LookActive = true;
                print("Active");
            }
        }
        else if (LookActive == true) 
        {
            if (lookatInteraction.looking)
            {
                print("Looking");
                if (MenuActivated == false)
                {
                    MenuActivation();
                    print("Activated");
                }
            }
            else if (lookatInteraction.looking == false)
            {
                print("Not Looking");
                if (MenuActivated == true)
                {
                    MenuDeactivation();
                    print("Deactivated");
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LookActive = false;
                print("InActive");
            }
        }
    }

    void MenuActivation()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("ActiveUI");

        foreach (GameObject go in gameObjectArray)
        {
            go.GetComponent<Renderer>().enabled = true;
        }
        if (fireflies.tag == "ActiveUI")
        {
            fireflies.Play();
        }
        MenuActivated = !MenuActivated;
    }
    void MenuDeactivation()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("ActiveUI");

        foreach (GameObject go in gameObjectArray)
        {
            go.GetComponent<Renderer>().enabled = false;
        }
        if (fireflies.tag == "ActiveUI")
        {
            fireflies.Stop();
        }
        MenuActivated = !MenuActivated;
    }
}
