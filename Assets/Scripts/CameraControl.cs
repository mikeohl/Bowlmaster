/* Camera control moves camera with ball down the lane until ball reaches pins */

using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Ball ball;
    public float cameraStopPosition = 1700;

    private Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = ball.transform.position - transform.position;
    }

    // Update is called once per frame
    // Move the Camera with the ball until it gets to a certain point before the pins
    void Update () {     
        if (ball.transform.position.z < cameraStopPosition) {
            transform.position = ball.transform.position - offset;
        }
    }
}
