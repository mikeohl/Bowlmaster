using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text standingDisplay;
    public float resetTime = 4.0f;

    private Ball ball;
    private PinSetter pinSetter;
    private PinCounter pinCounter;
    private bool ballOutOfPlay = false;

    private int standingCount = -1;
    private float standingCountUpdateTime;
    private int lastSettledCount = 10;

    private List<int> bowls = new List<int>();

    // Use this for initialization
    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballOutOfPlay)
        {
            standingDisplay.color = Color.red;
            UpdateStandingCount();
            if (Time.time - standingCountUpdateTime >= resetTime)
            {
                Bowl(lastSettledCount - standingCount);
            }
        }
    }

    public void SetBallOutOfPlay(bool isOutOfPlay)
    {
        ballOutOfPlay = isOutOfPlay;
    }

    // Reset lastStandingCount, ballEnteredBox, display color
    private void ResetBall()
    {
        ball.Reset();
        ballOutOfPlay = false;
        standingDisplay.color = Color.black;
    }

    // Update the standing count and last updated time if standingCount has changed
    private void UpdateStandingCount()
    {
        int currentCount = pinCounter.CountStandingPins();

        if (standingCount != currentCount)
        {
            standingCount = currentCount;
            standingCountUpdateTime = Time.time;
            standingDisplay.text = currentCount.ToString();
        }
    }

    private void Bowl (int pinsKnockedDown)
    {
        bowls.Add(pinsKnockedDown);

        ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
        pinSetter.SetPins(nextAction);

        lastSettledCount = standingCount;
        standingCount = -1;
        // Reset Ball
        ResetBall();

        if (nextAction == ActionMaster.Action.Reset)
        {
            lastSettledCount = 10;
        }
    }
}
