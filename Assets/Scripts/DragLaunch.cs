using UnityEngine;


[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private Vector3 startPosition, endPosition;
    private float startTime, endTime;
    
    // Use this for initialization
    void Start () {
        ball = GetComponent<Ball>();
    }

    // Capture time and position of drag start
    public void DragStart () {
        startTime = Time.time;
        startPosition = Input.mousePosition;
    }

    // Launch the ball
    public void DragEnd () {
        if (!ball.BallInPlay()) {
            endTime = Time.time;
            endPosition = Input.mousePosition;

            float dragDuration = startTime - endTime;
            Vector3 launchVelocity = new Vector3();
            launchVelocity.x = (endPosition.x - startPosition.x) / dragDuration / 5.0f;
            launchVelocity.z = (endPosition.y - startPosition.y) / dragDuration * 2.0f;

            launchVelocity.z = Mathf.Clamp(launchVelocity.z, 300.0f, 1500.0f);
            if (launchVelocity.x == double.NaN || launchVelocity.z == double.NaN) {
                Debug.LogWarning("Launch velocity contained NaN");
                return;
            }
            Debug.Log(launchVelocity.z);
            ball.Launch(launchVelocity);
        }
    }

    public void MoveStart (float xNudge) {
        if (!ball.BallInPlay() && ball.BallInLane()) {
            ball.transform.Translate(new Vector3(xNudge, 0, 0));
            if (!ball.BallInLane()) {
                ball.transform.Translate(new Vector3(-xNudge, 0, 0));
            }
        }
    }
}
