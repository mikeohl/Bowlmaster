using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsStanding ()
    {
        if (transform.rotation.eulerAngles.y != 0)
        {
            return true;
        }

        return true;
    }
}
