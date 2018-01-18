using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] scores, frames;
    public Text total;

    public void FillFrames(List<int> frameScores)
    {
        int i = 0;
        for (; i < frameScores.Count; i++)
        {
            frames[i].text = frameScores[i].ToString();
        }
        total.text = frameScores[i - 1].ToString();
    }

    public static string FormatRolls(List<int> rolls)
    {
        string scoresString = "";
        Queue<int> memory = new Queue<int>();

        foreach (int roll in rolls)
        {
            if (memory.Count == 0)
            {
                if (roll == 10)
                {
                    scoresString += "X";
                    if (scoresString.Length < 19) { scoresString += " "; }
                }
                else
                {
                    memory.Enqueue(roll);
                    if (roll == 0) { scoresString += "-"; }
                    else { scoresString += roll.ToString(); }
                }
            }
            else
            {
                int total = memory.Dequeue() + roll;
                if (total == 10) { scoresString += "/"; }
                else
                {
                    if (roll == 0) { scoresString += "-"; }
                    else { scoresString += roll.ToString(); }
                }
            }
        }
        return scoresString;
    }

    public void FillRollCard(List<int> rolls)
    {
        string scoresString = FormatRolls(rolls);
        for (int i = 0; i < scoresString.Length; i++)
        {
            scores[i].text = scoresString[i].ToString();
        }
        int j = 0;
        foreach (int score in ScoreMaster.ScoreCumulative(rolls))
        {
            frames[j].text = score.ToString();
            total.text = score.ToString();
            j++;
        }
    }
}
