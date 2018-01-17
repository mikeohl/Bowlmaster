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

    public void FillRolls(List<int> rolls)
    {
        //string scoresString = FillRollCard(rolls);
        //for (int i = 0; i < scoresString.Length; i++)
        //{
        //    scores[i].text = scoresString[i].ToString();
        //}
    }

    public void FillRollCard(List<int> rolls)
    {
        int i = 0;
        Queue<int> memory = new Queue<int>();

        foreach (int roll in rolls)
        {
            if (memory.Count == 0 && roll == 10)
            {
                if (i < 18) i++;
                scores[i].text = "X";
            }
            else if (memory.Count == 0)
            {
                if (roll == 0)
                {
                    scores[i].text = "-";
                }
                else
                {
                    scores[i].text = roll.ToString();
                }
                memory.Enqueue(roll);
            }
            else
            {
                int total = memory.Dequeue() + roll;
                if (total == 10)
                {
                    scores[i].text = "/";
                }
                else
                {
                    if (roll == 0)
                    {
                        scores[i].text = "-";
                    } else
                    {
                        scores[i].text = roll.ToString();
                    }
                }
            }
            i++;
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
