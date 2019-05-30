using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChange : MonoBehaviour
{

    public float xSpread;
    public float zSpread;
    public float yOffset;
    public Transform centerPoint;

    public float rotSpeed;
    public bool rotateClockwise;
    public bool activated = false;

    float timer = 0;
    private bool collision = false;

    public bool daylight = true;

    //public Material MoonMaterial;
    //public Material SunMaterial;

    Renderer rend;
    OVRGrabbable grabbedinfo;

    public GameObject Sun;
    public GameObject Moon;

    Renderer sunRend;
    Renderer moonRend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        grabbedinfo = GetComponent<OVRGrabbable>();
        sunRend = Sun.GetComponent<Renderer>();
        moonRend = Moon.GetComponent<Renderer>();
        moonRend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        rend.enabled = false;
        timer += Time.deltaTime * rotSpeed;
        if (gameObject.tag == "ActiveUI")
        {
            if (grabbedinfo.isGrabbed)
            {
                if ((grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
                {
                    if (daylight)
                    {
                        //rend.sharedMaterial = MoonMaterial;
                        sunRend.enabled = false;
                        moonRend.enabled = true;
                        Sun.tag = "Untagged";
                        Moon.tag = "ActiveUI";
                        print("Night Time");
                        daylight = false;
                    }
                    else if (daylight == false)
                    {
                        //rend.sharedMaterial = SunMaterial;
                        sunRend.enabled = true;
                        moonRend.enabled = false;
                        Sun.tag = "ActiveUI";
                        Moon.tag = "Untagged";
                        print("Day Time");
                        daylight = true;
                    }
                    activated = true;
                }
            }
            else
            {
                Rotate();
            }
        }
        else if (gameObject.tag == "UI")
        {
            if (grabbedinfo.isGrabbed)
            {
                if ((grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
                {
                    if (daylight)
                    {
                        //rend.sharedMaterial = MoonMaterial;
                        sunRend.enabled = false;
                        moonRend.enabled = true;
                        Sun.tag = "Untagged";
                        Moon.tag = "UI";
                        print("Night Time");
                        daylight = false;
                    }
                    else if (daylight == false)
                    {
                        //rend.sharedMaterial = SunMaterial;
                        sunRend.enabled = true;
                        moonRend.enabled = false;
                        Sun.tag = "UI";
                        Moon.tag = "Untagged";
                        print("Day Time");
                        daylight = true;
                    }
                    activated = true;
                }
            }
        }
        else
        {
            activated = false;
        }
        if (daylight)
        {
            Sun.tag = gameObject.tag;
        }
        else if (daylight == false)
        {
            Moon.tag = gameObject.tag;
        }
    }

    void Rotate()
    {
        if (rotateClockwise)
        {

            float x = -Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            float y = Mathf.Sin(timer) * yOffset;
            Vector3 pos = new Vector3(x, y, z);
            // Vector3 pos = new Vector3(x, yOffset, z); \\ Activate this and deactivate the line above in order to have a constant Y value
            // transform.position = pos + centerPoint.position;
            Vector3 targetPosition = pos + centerPoint.localPosition;
            Vector3 currentPosition = transform.localPosition;
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, rotSpeed * Time.deltaTime);
            transform.localPosition = newPosition;
        }
        else
        {
            float x = Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            float y = Mathf.Sin(timer) * yOffset;
            Vector3 pos = new Vector3(x, y, z);
            // Vector3 pos = new Vector3(x, yOffset, z); \\ Activate this and deactivate the line above in order to have a constant Y value 
            // transform.position = pos + centerPoint.position;
            Vector3 targetPosition = pos + centerPoint.localPosition;
            Vector3 currentPosition = transform.localPosition;
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, rotSpeed * Time.deltaTime);
            transform.localPosition = newPosition;
        }
        transform.LookAt(centerPoint);
    }
}

