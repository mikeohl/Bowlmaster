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
	}

    private void Update()
    {
        if (inPlay)
        {
            if (rigidBody.velocity.z < 300f)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0.0f, 300.0f);
            }
        }
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
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;
        inPlay = false;
    }
}
