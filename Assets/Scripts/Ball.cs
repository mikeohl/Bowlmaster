/* Ball controls ball launch and launch positon */

using UnityEngine;

public class Ball : MonoBehaviour {

    const float LANE_WIDTH = 104f;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Vector3 launchVelocity;
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

    // Update is called once per frame
    // [Hack] Maintain launch velocity in z direction
    // Launch z velocity currently depreciates as ball contacts the floor
    private void Update() {
        if (inPlay) {
            if (rigidBody.velocity.z != launchVelocity.z) {
                rigidBody.velocity = launchVelocity;
            }
        }
    }

    // Launch ball with passed velocity vector. 
    // Ball is inPlay after launch
    public void Launch (Vector3 velocity) {
        // Protect again NaN velocity. Only launch with valid velocity
        if (velocity.x == double.NaN || velocity.z == double.NaN) {
            return;
        }
        rigidBody.useGravity = true;
        launchVelocity = velocity;
        rigidBody.velocity = velocity;

        // [DEV] Add angular velocity if ball is positioned off center
        if(rigidBody.position.x > 0.5) {
            rigidBody.angularVelocity = new Vector3(45f, 0f, 0f);
        } else if(rigidBody.position.x < 0.5) {
            rigidBody.angularVelocity = new Vector3(-45f, 0f, 0f);
        }
        
        inPlay = true;
        PlayRollSound();
    }

    // Check if ball is within the lane to prevent setting
    // ball position out of lane at start
    public bool BallInLane () {
        if (transform.position.x > -(LANE_WIDTH/2)
            && transform.position.x < LANE_WIDTH/2) {
            return true;
        }
        return false;
    }

    // Reset the ball to launch position for another roll
    public void Reset () {
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;
        inPlay = false;
    }

    public void PlayRollSound () {
        audioSource.Play();
    }

    public void StopRollSound () {
        audioSource.Stop();
    }

    public bool BallInPlay() {
        return inPlay;
    }
}
