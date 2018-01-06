using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Ball ball;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = ball.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        // Move the Camera with the ball until it gets to a certain point before the pins
        if (transform.position.z < 1700)
        {
            transform.position = ball.transform.position - offset;
        }
	}
}
