using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] scores, frames;
    public Text total;

    // Generate a string of bowl symbols and numbers for UI score card
    // given a list of rolls.
    public static string FormatRolls(List<int> rolls) {
        string scoresString = "";
        Queue<int> memory = new Queue<int>();

        foreach (int roll in rolls) {
            if (memory.Count == 0) {
                if (roll == 10) {
                    scoresString += "X";
                    if (scoresString.Length < 19) { scoresString += " "; }
                } else {
                    memory.Enqueue(roll);
                    if (roll == 0) { scoresString += "-"; }
                    else { scoresString += roll.ToString(); }
                }
            } else {
                int total = memory.Dequeue() + roll;
                if (total == 10) { scoresString += "/"; }
                else {
                    if (roll == 0) { scoresString += "-"; }
                    else { scoresString += roll.ToString(); }
                }
            }
        }
        return scoresString;
    }

    // Fill the UI Roll Card with bowl counts, symbols, and cumulative scores
    public void FillRollCard(List<int> rolls) {
        string scoresString = FormatRolls(rolls);
        for (int i = 0; i < scoresString.Length; i++) {
            scores[i].text = scoresString[i].ToString();
        }
        int j = 0;
        foreach (int score in ScoreMaster.ScoreCumulative(rolls)) {
            frames[j].text = score.ToString();
            total.text = score.ToString();
            j++;
        }
    }
}
