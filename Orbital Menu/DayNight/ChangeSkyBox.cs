using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour {

    public Material nightSkyBox;
    public Material daySkyBox;
    SkyBoxChange skyboxchange;
    public Light directionalLight;
	// Use this for initialization
	void Start () {
        skyboxchange = GameObject.Find("LightChange").GetComponent<SkyBoxChange>();
        //directionalLight = GameObject.Find("DirectionalLight").GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        if (skyboxchange.activated)
        {
            if (skyboxchange.daylight == false)
            {
                RenderSettings.skybox = nightSkyBox;
                directionalLight.intensity = 0.45f;
            }
            else if (skyboxchange.daylight)
            {
                RenderSettings.skybox = daySkyBox;
                directionalLight.intensity = 1.0f;
            }
        }
    }
}
