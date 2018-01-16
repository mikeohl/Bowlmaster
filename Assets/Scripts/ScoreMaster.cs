using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    // Returns a list of individual frame scores, NOT cumulative.
    public static List<int> ScoreFrames (List<int> rolls)
    {
        List<int> frameList = new List<int>();
        Queue<int> memory = new Queue<int>();
        
        foreach (int roll in rolls)
        {
            memory.Enqueue(roll);
            if (memory.Peek() < 10 && memory.Count == 2 && roll < 10) 
            {
                if (memory.Peek() + roll < 10)
                {
                    frameList.Add(memory.Peek() + roll);
                    memory.Clear();
                }
            } else if (memory.Peek() < 10 && memory.Count == 3) // We have a spare to be scored
            {
                frameList.Add(memory.Dequeue() + memory.Dequeue() + memory.Peek());
            } else if (memory.Peek() == 10 && memory.Count == 3) // We have a strike to be scored
            {
                frameList.Add(memory.Dequeue() + memory.Peek() + roll);
                if (memory.Peek() + roll < 10 && frameList.Count != 10)
                {
                    frameList.Add(memory.Peek() + roll);
                    memory.Clear();
                }
            }
        }

        return frameList;
    }

    // Returns a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls))
        {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }
}
