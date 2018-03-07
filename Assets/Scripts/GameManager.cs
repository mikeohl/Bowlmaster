/* GameManager manages the game once the player has made a roll.
 * It detects if the ball is out of play and pins have settled, 
 * then scores the roll, resets the ball and performs the next
 * lane action. Finally, it loads the end screen once the game
 * is over.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public LevelManager levelManager;
    public Text standingDisplay;
    public float resetTime = 4.0f;

    private Ball ball;
    private PinSetter pinSetter;
    private PinCounter pinCounter;
    private ScoreDisplay scoreDisplay;
    private bool ballOutOfPlay = false;

    private int standingCount = -1;
    private float standingCountUpdateTime;
    private int lastSettledCount = 10;

    private List<int> bowls = new List<int>();

    // Use this for initialization
    void Start() {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }

    // Update is called once per frame
    // When ball leaves the lane, stop ball roll sound and count pins 
    // knocked down after they have settled. UI pin count turns red
    // until settled and then black.
    void Update() {
        if (ballOutOfPlay) {
            standingDisplay.color = Color.red;
            ball.StopRollSound();
            UpdateStandingCount();

            // After developer set resetTime...
            if (Time.time - standingCountUpdateTime >= resetTime) {
                standingDisplay.color = Color.black;
                ScoreBowl(lastSettledCount - standingCount);
                UpdateCounts();
                ResetBall();
                NextAction();
            } 
        }
    }

    // 
    public void SetBallOutOfPlay(bool isOutOfPlay) {
        ballOutOfPlay = isOutOfPlay;
    }

    // Update the pin standing count and last updated time if 
    // standingCount has changed.
    private void UpdateStandingCount() {
        int currentCount = pinCounter.CountStandingPins();

        if (standingCount != currentCount) {
            standingCount = currentCount;
            standingCountUpdateTime = Time.time;
            standingDisplay.text = currentCount.ToString();
        }
    }

    // Update lastSettledCount and standingCount. 
    private void UpdateCounts () {
        lastSettledCount = standingCount;
        standingCount = -1;
    }

    // Add bowl to list of bowls and display score of the bowl
    private void ScoreBowl (int pinsKnockedDown) {

        // Add bowl to list and display scores in Score Card
        bowls.Add(pinsKnockedDown);
        try { scoreDisplay.FillRollCard(bowls); }
        catch { Debug.Log("FillRollCard Failed!"); }   
    }

    private void ResetBall () {
        ball.Reset();
        ballOutOfPlay = false;
    }

    // Lane performs next action based on list of bowls and
    // handles edge case actions
    private void NextAction () {
        // Get next action and set pins
        ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
        pinSetter.SetPins(nextAction);

        if (nextAction != ActionMaster.Action.Tidy) {
            lastSettledCount = 10;
        }
        if (nextAction == ActionMaster.Action.EndGame) {
            Invoke("LoadEndScreen", 5);
        }
    }

    // End screen is loaded with Invoke when we get EndGame
    private void LoadEndScreen () {
        levelManager.LoadNextLevel();
    }
}
