using System.Collections.Generic;

public static class ScoreMaster {

    // Returns a list of individual frame scores
    public static List<int> ScoreFrames(List<int> rolls) {
        List<int> frameList = new List<int>();
        Queue<int> memory = new Queue<int>();

        foreach (int roll in rolls) {
            memory.Enqueue(roll);
            if (memory.Count == 3)  {
                if (memory.Peek() == 10) { // We have a STRIKE to be scored
                    frameList.Add(memory.Dequeue() + memory.Peek() + roll);
                }
                else { // We have a SPARE to be scored
                    frameList.Add(memory.Dequeue() + memory.Dequeue() + memory.Peek());
                } 
            }
            if (memory.Count == 2 && memory.Peek() + roll < 10 && frameList.Count < 10) { // We have an OPEN frame to be scored
                frameList.Add(memory.Peek() + roll);
                memory.Clear();
            }
        }

        return frameList;
    }

    // Returns a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative(List<int> rolls) {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls)) {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }
}
