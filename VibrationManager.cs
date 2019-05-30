using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour {

    public static VibrationManager singleton;

	// Use this for initialization
	void Start () {
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;
	}
	
    public void TriggerVibration(int iteration, int frequency, int strenght, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for(int i = 0; i < iteration; i ++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strenght : (byte)0);
        }
        
        if (controller == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        if (controller == OVRInput.Controller.RTouch)
        {
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
}
