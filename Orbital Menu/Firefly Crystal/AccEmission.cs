using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccEmission : MonoBehaviour
{

    Vector3 newVelocity;
    Vector3 oldVelocity;
    Vector3 accelerationVector;
    float acceleration;
    float oldacceleration;
    float emissioncount;
    float oldemissioncount;

    Rigidbody rigidBody;
    public ParticleSystem particles;
    ParticleSystem.EmissionModule emissionModule;
    bool pass = true;

    public float xSpread;
    public float zSpread;
    public float yOffset;
    public Transform centerPoint;

    public float rotSpeed;
    public bool rotateClockwise;

    float timer = 0;
    OVRGrabbable grabbedinfo;

    //Tree fireflires
    public ParticleSystem treefireflies;
    ParticleSystem.EmissionModule treefirefliesEmission;
    public ParticleSystem treefireflies2;
    ParticleSystem.EmissionModule treefirefliesEmission2;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        emissionModule = particles.emission;
        grabbedinfo = GetComponent<OVRGrabbable>();
        treefirefliesEmission = treefireflies.emission;
        treefirefliesEmission2 = treefireflies2.emission;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * rotSpeed;
        if (gameObject.tag == "ActiveUI")
        {
            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(5.00f);
            treefirefliesEmission.rateOverTime = new ParticleSystem.MinMaxCurve(5.00f);
            treefirefliesEmission2.rateOverTime = new ParticleSystem.MinMaxCurve(5.00f);
            if (grabbedinfo.isGrabbed)
            {
                if ((grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) || (grabbedinfo.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch)))
                {

                }
            }
            else
            {
                Rotate();
            }
        }
        else if (gameObject.tag == "UI")
        {
            newVelocity = rigidBody.velocity;
            accelerationVector = (newVelocity - oldVelocity) / Time.fixedDeltaTime;
            acceleration = Vector3.Magnitude(accelerationVector);
            //print(acceleration);
            oldVelocity = newVelocity;
            if (acceleration > 20.00f)
            {
                if (acceleration > (oldacceleration + 8.0f))
                {
                    print("YES");
                    float normalizedAcc = Mathf.InverseLerp(20.00f, 80.00f, acceleration);
                    //print(normalizedAcc);
                    emissioncount = Mathf.Lerp(5.0f, 50.0f, normalizedAcc);
                    print(emissioncount);
                    if (emissioncount > oldemissioncount)
                    {
                        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Round(emissioncount));
                        treefirefliesEmission.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Round(emissioncount));
                        treefirefliesEmission2.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Round(emissioncount));
                    }
                }
            }
            else
            {
                if (emissioncount > 5.0f)
                {
                    emissioncount = emissioncount - 0.01f;
                    emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Round(emissioncount));
                    treefirefliesEmission.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Round(emissioncount));
                    treefirefliesEmission2.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Round(emissioncount));
                }
            }
            oldacceleration = acceleration;
            oldemissioncount = emissioncount;
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
        //transform.LookAt(centerPoint);
    }
}
