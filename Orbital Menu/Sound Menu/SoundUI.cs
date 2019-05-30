using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundUI : MonoBehaviour
{

    Color color;
    Transform m_Transform;
    float radius = 0.5f;
    public GameObject SoundController;
    float resultX = 0.60f;
    float resultY = 0.40f;
    float resultZ = 0.50f;
    float oldresultX = 0.60f;
    float oldresultY = 0.40f;
    float oldresultZ = 0.50f;
    public AudioSource treesound;
    public AudioSource firesound;
    public AudioSource watersound;
    //public AudioSource watersound2;
    OVRGrabbable controllerGrabbable;
    public bool FreeControl = true;

    public GameObject ControlX;
    public GameObject ControlY;
    public GameObject ControlZ;

    ControlXScript ControlXState;
    ControlYScript ControlYState;
    ControlZScript ControlZState;
    SoundControlScript SoundControllState;

    public List<GameObject> InActiveTree = new List<GameObject>();
    public List<GameObject> ActiveTree = new List<GameObject>();

    float normalizedValueX;
    float normalizedValueY;
    float normalizedValueZ;

    public ParticleSystem fireParticles;
    public GameObject fire;
    public Light fireLight;

    // Use this for initialization
    void Start()
    {
        color.a = 0.45f;
        m_Transform = GetComponent<Transform>();
        treesound = GameObject.Find("Trees").GetComponent<AudioSource>();
        firesound = GameObject.Find("CampFire").GetComponent<AudioSource>();
        watersound = GameObject.Find("WaterSound").GetComponent<AudioSource>();
        //watersound2 = GameObject.Find("WaterSound2").GetComponent<AudioSource>();
        SoundControllState = GameObject.Find("SoundControl").GetComponent<SoundControlScript>();
        ControlXState = GameObject.Find("ControlX").GetComponent<ControlXScript>();
        ControlYState = GameObject.Find("ControlY").GetComponent<ControlYScript>();
        ControlZState = GameObject.Find("ControlZ").GetComponent<ControlZScript>();
        //
        ControlX.GetComponent<Renderer>().enabled = false;
        ControlY.GetComponent<Renderer>().enabled = false;
        ControlZ.GetComponent<Renderer>().enabled = false;
        // Tree count control 
        GameObject[] TreeArray = GameObject.FindGameObjectsWithTag("Tree");
        ActiveTree.Clear();
        InActiveTree.Clear();
        foreach (GameObject i in TreeArray)
        {
            if (i.activeSelf == false)
            {
                InActiveTree.Add(i);
            }
            else if (i.activeSelf)
            {
                ActiveTree.Add(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FreeControl == true)
        {
            Vector3 CenterOfSphere = m_Transform.localPosition;
            CenterOfSphere.y = 0.00f;
            //print("Center of Sphere: " + CenterOfSphere);
            Vector3 SoundControllerCenter = SoundController.transform.localPosition;
            //print("Center of Controller: " + SoundControllerCenter);
            Vector3 FromCenterToSoundController = SoundControllerCenter - CenterOfSphere;
            //print("Relative Controller Center: " + FromCenterToSoundController.ToString("f3"));
            float xAbsolute = Mathf.Abs(FromCenterToSoundController.x);

            //Distance Percentage
            normalizedValueX = Mathf.InverseLerp(0.00f, 0.45f, xAbsolute);
            //Volume Percentage in Logarithmic scale
            resultX = Mathf.InverseLerp(-25.00f, 1.00f, (Mathf.Log10(normalizedValueX) * 20));

            float yAbsolute = Mathf.Abs(FromCenterToSoundController.y);
            normalizedValueY = Mathf.InverseLerp(0, 0.45f, yAbsolute);
            resultY = Mathf.InverseLerp(-25.00f, 1.00f, (Mathf.Log10(normalizedValueY) * 20));
            float zAbsolute = Mathf.Abs(FromCenterToSoundController.z);
            normalizedValueZ = Mathf.InverseLerp(0, 0.45f, zAbsolute);
            resultZ = Mathf.InverseLerp(-25.00f, 1.00f, (Mathf.Log10(normalizedValueZ) * 20));
            treesound.volume = resultY;
            firesound.volume = resultX;
            watersound.volume = resultZ * 0.1f;
            //watersound2.volume = resultZ*0.1f;
            EnvironmentUpdate();
            FreeControlSwap();
        }
        else if (FreeControl == false)
        {
            Vector3 CenterOfSphere = m_Transform.localPosition;
            CenterOfSphere.y = 0.00f;
            Vector3 controlX = ControlX.transform.localPosition;
            Vector3 controlY = ControlY.transform.localPosition;
            Vector3 controlZ = ControlZ.transform.localPosition;
            float xAbsolute = Mathf.Abs(controlX.x);
            normalizedValueX = Mathf.InverseLerp(0.00f, 0.45f, xAbsolute);
            resultX = Mathf.InverseLerp(-25.00f, 1.00f, (Mathf.Log10(normalizedValueX) * 20));
            float yAbsolute = Mathf.Abs(controlY.y);
            normalizedValueY = Mathf.InverseLerp(0, 0.45f, yAbsolute);
            resultY = Mathf.InverseLerp(-25.00f, 1.00f, (Mathf.Log10(normalizedValueY) * 20));
            float zAbsolute = Mathf.Abs(controlZ.z);
            normalizedValueZ = Mathf.InverseLerp(0, 0.45f, zAbsolute);
            resultZ = Mathf.InverseLerp(-25.00f, 1.00f, (Mathf.Log10(normalizedValueZ) * 20));
            treesound.volume = resultY;
            firesound.volume = resultX;
            watersound.volume = resultZ * 0.1f;
            //watersound2.volume = resultZ*0.1f;
            EnvironmentUpdate();
            AxisControlSwap();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "SoundUI")
        {
            //VibrationManager.singleton.TriggerVibration(20, 2, 255, controllerGrabbable.grabbedBy.m_controller);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "SoundUI")
        {
            //VibrationManager.singleton.TriggerVibration(20, 2, 255, controllerGrabbable.grabbedBy.m_controller);
        }
    }
    void FreeControlSwap()
    {
        if (SoundControllState.Active == true)
        {
            print("TRUER");
            Vector3 SoundControllerCenterChange = SoundController.transform.localPosition;
            ControlX.transform.localPosition = new Vector3(SoundControllerCenterChange.x, 0, 0);
            ControlY.transform.localPosition = new Vector3(0, SoundControllerCenterChange.y, 0);
            ControlZ.transform.localPosition = new Vector3(0, 0, SoundControllerCenterChange.z);
            SoundController.GetComponent<Renderer>().enabled = false;
            ControlX.GetComponent<Renderer>().enabled = true;
            ControlY.GetComponent<Renderer>().enabled = true;
            ControlZ.GetComponent<Renderer>().enabled = true;
            FreeControl = false;
            SoundControllState.Active = false;
        }
    }
    void AxisControlSwap()
    {
        //if (controlXGrabbable.grabbedBy.m_controller == OVRInput.Controller.RTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        //{
        //FromAxisToFree();
        //}
        //else if (controlXGrabbable.grabbedBy.m_controller == OVRInput.Controller.LTouch && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        //{
        // FromAxisToFree();
        //}
        if (ControlXState.XActive == true)
        {
            FromAxisToFree();
            ControlXState.XActive = false;
        }
        else if (ControlYState.YActive == true)
        {
            FromAxisToFree();
            ControlYState.YActive = false;
        }
        else if (ControlZState.ZActive == true)
        {
            FromAxisToFree();
            ControlZState.ZActive = false;
        }
    }
    void FromAxisToFree()
    {
        Vector3 controlXChange = ControlX.transform.localPosition;
        Vector3 controlYChange = ControlY.transform.localPosition;
        Vector3 controlZChange = ControlZ.transform.localPosition;
        SoundController.transform.localPosition = new Vector3(controlXChange.x, controlYChange.y, controlZChange.z);
        ControlX.GetComponent<Renderer>().enabled = false;
        ControlY.GetComponent<Renderer>().enabled = false;
        ControlZ.GetComponent<Renderer>().enabled = false;
        SoundController.GetComponent<Renderer>().enabled = true;
        FreeControl = true;
    }
    void EnvironmentUpdate()
    {
        UpdatingTrees();
        UpdatingFire();
    }

    void UpdatingFire()
    {
        float scalevalue = Mathf.Lerp(0.3f, 0.8f, normalizedValueX);
        print(scalevalue);
        if (normalizedValueX <= 0.08f)
        {
            fireParticles.Stop();
            fireLight.enabled = false;
        }
        else
        {
            if (fireParticles.isStopped)
            {
                fireParticles.Play();
            }
            fire.transform.localScale = new Vector3(scalevalue, scalevalue, scalevalue);
            fireLight.intensity = scalevalue;
        }
    }

    void UpdatingTrees()
    {
        if (normalizedValueY >= 0.1f && normalizedValueY <= 0.25f)
        {
            int treeCount = 1;
            UpdatingTreeCount(treeCount);
        }
        else if (normalizedValueY > 0.25f && normalizedValueY <= 0.40f)
        {
            int treeCount = 2;
            UpdatingTreeCount(treeCount);
        }
        else if (normalizedValueY > 0.40f && normalizedValueY <= 0.55f)
        {
            int treeCount = 3;
            UpdatingTreeCount(treeCount);
        }
        else if (normalizedValueY > 0.55f && normalizedValueY <= 0.70f)
        {
            int treeCount = 4;
            UpdatingTreeCount(treeCount);
        }
        else if (normalizedValueY > 0.70f && normalizedValueY <= 0.85f)
        {
            int treeCount = 5;
            UpdatingTreeCount(treeCount);
        }
        else if (normalizedValueY > 0.85f && normalizedValueY <= 1.00f)
        {
            int treeCount = 6;
            UpdatingTreeCount(treeCount);
        }
        else if (normalizedValueY >= 0.00f && normalizedValueY <= 0.1f)
        {
            int treeCount = 0;
            foreach (GameObject go in ActiveTree)
            {
                go.GetComponent<Renderer>().enabled = false;
            }
        }
        oldresultY = normalizedValueY;
    }
    void UpdatingTreeCount(int TreeCount)
    {
        int num = ActiveTree.Count - TreeCount;
        if (num == 0)
        {
            // Nothing needs to be done.
        }
        else if (num < 0)
        {
            num = Mathf.Abs(num);
            for (int i = 0; i < num; i++)
            {
                int randNum = Random.Range(0, InActiveTree.Count);
                GameObject activatingTree = InActiveTree[randNum];
                activatingTree.SetActive(true);
                InActiveTree.Remove(activatingTree);
                ActiveTree.Add(activatingTree);
            }
        }
        else if (num > 0)
        {
            for (int i = 0; i < num; i++)
            {
                int randNum = Random.Range(0, ActiveTree.Count);
                GameObject deactivatingTree = ActiveTree[randNum];
                deactivatingTree.SetActive(false);
                ActiveTree.Remove(deactivatingTree);
                InActiveTree.Add(deactivatingTree);
            }
        }
    }
}

