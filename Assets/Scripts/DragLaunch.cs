using System.Collections;
using System.Collections.Generic;
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
	
	// Update is called once per frame
	void Update () {
		
	}

    // Capture time and position of drag start
    public void DragStart ()
    {
        startTime = Time.time;
        startPosition = Input.mousePosition;
    }

    // Launch the ball
    public void DragEnd ()
    {
        if (!ball.BallInPlay())
        {
            endTime = Time.time;
            endPosition = Input.mousePosition;

            float dragDuration = startTime - endTime;
            Vector3 launchVelocity = new Vector3();
            launchVelocity.x = (endPosition.x - startPosition.x) / dragDuration / 4.0f;
            launchVelocity.z = (endPosition.y - startPosition.y) / dragDuration * 2.5f;

            launchVelocity.z = Mathf.Clamp(launchVelocity.z, 1000.0f, 2000.0f);
            Debug.Log(launchVelocity.z);
            ball.Launch(launchVelocity);
        }
    }

    // TODO: Needs to have ball local coordinates reset for this to work consistently
    public void MoveStart (float xNudge)
    {
        if (!ball.BallInPlay() && ball.BallInLane())
        {
            ball.transform.Translate(new Vector3(xNudge, 0, 0));
        }
        //if (ball.transform.position.x > 52.0f)
        //{
        //    ball.transform.position.x = 5;
        //}
        
        Mathf.Clamp(ball.transform.position.x, -52.0f, 52.0f);
        Debug.Log("Ball moved " + xNudge);
        // ball.transform.position.x += xNudge;
    }
}
