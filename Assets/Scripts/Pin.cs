using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        print(name + IsStanding());
	}

    public bool IsStanding ()
    {
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        // print(name + eulerRotation);

        float xTilt = ((eulerRotation.x + standingThreshold) % 360.0f) / 2.0f;
        float zTilt = ((eulerRotation.z + standingThreshold) % 360.0f) / 2.0f;

        if (xTilt < standingThreshold && zTilt < standingThreshold)
        {
            return true;
        }

        return false;
    }
}
