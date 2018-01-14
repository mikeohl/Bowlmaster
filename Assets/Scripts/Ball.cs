using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Vector3 startPos;
    private bool inPlay;
    
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;

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

    public void Reset ()
    {
        Debug.Log("Resetting Ball");
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;
        inPlay = false;
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
        //if (rigidBody.velocity.z < launchVelocity.z)
        //{
        //    rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, launchVelocity.z);
        //    print(rigidBody.velocity);
        //}
	}
}
