/* DragLaunch calculates launch velocity from mouse drag on click
 * and release. Ball position before launch can also be set. 
 */

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

    // Position the ball across the lane (x-axis) before launch
    public void MoveStart(float xNudge) {
        if (!ball.BallInPlay() && ball.BallInLane()) {
            ball.transform.Translate(new Vector3(xNudge, 0, 0));
            if (!ball.BallInLane()) {
                ball.transform.Translate(new Vector3(-xNudge, 0, 0));
            }
        }
    }

    // Capture time and position of drag start
    public void DragStart () {
        startTime = Time.time;
        startPosition = Input.mousePosition;
    }

    // Capture time and position of drag end and Launch the ball.
    // Ball velocity is calculated based on drag distance over time.
    // Launch velocity is clamped to 'reasonable' velocity
    public void DragEnd () {
        if (!ball.BallInPlay()) {
            endTime = Time.time;
            endPosition = Input.mousePosition;

            float dragDuration = startTime - endTime;
            Vector3 launchVelocity = new Vector3();
            launchVelocity.x = (endPosition.x - startPosition.x) / dragDuration / 5.0f;
            launchVelocity.z = (endPosition.y - startPosition.y) / dragDuration * 2.0f;

            // Clamp to 'reasonable' velocity
            launchVelocity.z = Mathf.Clamp(launchVelocity.z, 300.0f, 1250.0f);

            // Protect again NaN result. Only launch with valid velocity
            if (launchVelocity.x != double.NaN && launchVelocity.z != double.NaN) {
                ball.Launch(launchVelocity);
                return;
            }
            Debug.LogWarning("Launch velocity contained NaN");
            return;
        }
    }
}
