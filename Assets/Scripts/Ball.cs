using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    private bool inPlay;
    
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rigidBody.useGravity = false;
        inPlay = false;

        // Launch(launchVelocity);
	}

    public void Launch (Vector3 velocity)
    {
        rigidBody.useGravity = true;
        rigidBody.velocity = velocity;
        inPlay = true;
        Roll();
    }

    public void Roll ()
    {
        audioSource.Play();
    }

    public bool BallInPlay ()
    {
        return inPlay;
    }

    public bool BallInLane ()
    {
        if (transform.position.x > -52.0f && transform.position.x < 52.0f)
        {
            return true;
        }
        return false;
    }

    //public void KeepBallOnLane ()
    //{
    //    if (transform.position.x <= -52.0f)
    //    {
    //        transform.position.x = 5.0f;
    //    }
    //}
	
	// Update is called once per frame
	void Update () {
		
	}
}
