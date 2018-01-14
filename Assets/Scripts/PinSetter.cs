using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    // public Pin [] pins;
    public GameObject pins;
    public Text standingDisplay;
    public float resetTime      = 4.0f;
    public float pinRaiseHeight = 40.0f;

    private Ball ball;
    private Animator animator;
    private ActionMaster actionMaster;

    private int lastSettledCount = 10;
    private int standingCount    = -1;
    private float standingCountUpdateTime;
    private bool ballOutOfPlay = false;

    // Use this for initialization
    void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GameObject.FindObjectOfType<Animator>();
        actionMaster = new ActionMaster();
    }
	
	// Update is called once per frame
	void Update () {
        if (ballOutOfPlay)
        {
            standingDisplay.color = Color.red;
            Bowl();
        }
	}

    // Count the number of upright pins
    public int CountStandingPins ()
    {
        // Assume NO pins are standing
        int standingPins = 0;

        // Loop through pins, check if standing and update count
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standingPins++;
            }
        }
        return standingPins;
    }

    // Count fallen pins

    // Update the standing count and last updated time if standingCount has changed
    private void UpdateStandingCount ()
    {
        int currentCount = CountStandingPins();

        if (standingCount != currentCount)
        {
            standingCount = currentCount;
            standingCountUpdateTime = Time.time;
            standingDisplay.text = currentCount.ToString();
        }
    }

    // Reset lastStandingCount, ballEnteredBox, display color
    private void ResetBall ()
    {
        ball.Reset();
        ballOutOfPlay = false;
        standingDisplay.color = Color.black;
    }

    private void Bowl ()
    {
        UpdateStandingCount();
        if (Time.time - standingCountUpdateTime >= resetTime)
        {
            // Update Bowl Score
            ActionMaster.Action action = actionMaster.Bowl(lastSettledCount - standingCount);
            Debug.Log(action);
            switch (action)
            {
                case ActionMaster.Action.Tidy:
                    animator.SetTrigger("tidyTrigger");
                    break;

                case ActionMaster.Action.EndGame:
                case ActionMaster.Action.EndTurn:
                case ActionMaster.Action.Reset:
                    animator.SetTrigger("resetTrigger");
                    break;
            }
            lastSettledCount = standingCount;
            standingCount = -1;
            // Reset Ball
            ResetBall();
        }
    }

    // Raise Pins to avoid swiper
    public void RaisePins ()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                pin.transform.Translate(new Vector3(0.0f, pinRaiseHeight, 0.0f), Space.World);
                pin.DisableGravity();
                pin.Stabilize();
            }
        }
    }

    // Lower pins back down to the lane
    public void LowerPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.transform.Translate(new Vector3(0.0f, -pinRaiseHeight, 0.0f), Space.World);
            pin.EnableGravity();
            pin.Stabilize();
        }
    }

    // Create a new set of 10 pins to be placed on the lane
    public void RenewPins ()
    {
        Instantiate(pins);
        RaisePins();
        lastSettledCount = 10;
        standingDisplay.text = lastSettledCount.ToString();
    }

    // Eliminate Pins that leave the Pin Setter box
    public void OnTriggerExit(Collider collider)
    {
        // Attempt to get Pin object from collider
        Pin pin = collider.gameObject.GetComponentInParent<Pin>();

        // If colliding object was pin, destroy child objects and then the pin itself.
        // Otherwise, destroy the gameobject of the collider.
        if (pin)
        {
            foreach (Transform child in pin.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pin.gameObject);
        }
        else
        {
            //Destroy(collider.gameObject);
        }
    }

    public void SetBallOutOfPlay(bool isOutOfPlay)
    {
        ballOutOfPlay = isOutOfPlay;
    }
}
