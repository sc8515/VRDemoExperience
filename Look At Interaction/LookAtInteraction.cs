using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtInteraction : MonoBehaviour {

    Ray rayCasting;
    public float sightlength;
    public bool looking = false;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //RaycastHit seen;
        //Ray raydirection = new Ray(transform.position, transform.forward);
        //if (Physics.Raycast(raydirection, out seen, sightlength))
        //{
            //if (seen.collider.tag == "CoreUI") //in the editor, tag anything you want to interact with and use it here
            //{
                //print("HIT!!!");
            //}

        //}
        //Debug.DrawRay(transform.position, transform.forward, Color.black, 1); //unless you allow debug to be seen in game, this will only be viewable in the scene view
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "CoreUI")
        {
            looking = true;
            print("Cone Hit!!");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "CoreUI")
        {
            looking = false;
        }
    }
}

