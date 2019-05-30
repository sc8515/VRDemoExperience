using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SoundUIActivation : MonoBehaviour {

    bool SoundMenuActivated = false;
    OVRGrabbable grabbedinfo;
    RotationConstraint rotationConstraint;
    public bool constraintActive;
    Transform diskTransform;

    public float xSpread;
    public float zSpread;
    public float yOffset;
    public Transform centerPoint;

    public float rotSpeed;
    public bool rotateClockwise;

    float timer = 0;
    SoundUI Stage;
    bool boundarySphereRenderer;
    // Use this for initialization
    void Start () {
        grabbedinfo = GetComponent<OVRGrabbable>();
        rotationConstraint = GetComponent<RotationConstraint>();
        diskTransform = GetComponent<Transform>();
        SoundMenuDeactivation();
        Stage = GameObject.Find("BoundarySphere").GetComponent<SoundUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * rotSpeed;
        if (gameObject.tag == "ActiveUI")
        {
            if (SoundMenuActivated)
            {
                SoundMenuDeactivation();
                rotationConstraint.enabled = false;
            }
            if (grabbedinfo.isGrabbed)
            {

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
                    if (SoundMenuActivated)
                    {
                        SoundMenuDeactivation();
                        rotationConstraint.enabled = false;
                    }
                    else if (SoundMenuActivated == false)
                    {
                        SoundMenuActivation();
                        diskTransform.eulerAngles = new Vector3(0, 0, 0);
                        rotationConstraint.enabled = true;
                    }
                }
            }
        }
    }
    void SoundMenuActivation()
    {
        boundarySphereRenderer = GameObject.Find("BoundarySphere").GetComponent<Renderer>().enabled = true;
        if (Stage.FreeControl)
        {
            Renderer soundControlRenderer = GameObject.Find("SoundControl").GetComponent<Renderer>();
            soundControlRenderer.enabled = true;
            SoundMenuActivated = !SoundMenuActivated;
        }
        else if (Stage.FreeControl == false)
        {
            Renderer controlXRenderer = GameObject.Find("ControlX").GetComponent<Renderer>();
            Renderer controlYRenderer = GameObject.Find("ControlY").GetComponent<Renderer>();
            Renderer controlZRenderer = GameObject.Find("ControlZ").GetComponent<Renderer>();
            controlXRenderer.enabled = true;
            controlYRenderer.enabled = true;
            controlZRenderer.enabled = true;
            SoundMenuActivated = !SoundMenuActivated;
        }
    }
    void SoundMenuDeactivation()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("SoundUI");

        foreach (GameObject go in gameObjectArray)
        {
            go.GetComponent<Renderer>().enabled = false;
        }
        boundarySphereRenderer = GameObject.Find("BoundarySphere").GetComponent<Renderer>().enabled = false;
        SoundMenuActivated = !SoundMenuActivated;
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
