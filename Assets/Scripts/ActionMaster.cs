using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};

    // private Frame[] frames = new Frame[10];
    private int bowl = 0;
    private bool firstRoll = true;
    private int firstScore = 0;

    public static Action NextAction (List<int> pinsKnockedDown)
    {
        ActionMaster actionMaster = new ActionMaster();
        Action nextAction = Action.EndTurn;
        foreach (int pins in pinsKnockedDown)
        {
            nextAction = actionMaster.Bowl(pins);
        }

        return nextAction;
    }

    private Action Bowl (int pins)
    {
        // Handle invalid pin number
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("Invalid pin count. Pins must be 0 - 10");
        }

        // Frame 10
        if (bowl > 17)
        {
            // If on third roll, end game
            if (bowl == 20)
            {
                bowl = 0;
                return Action.EndGame;
            } // If on roll 1 or 2 & strike, reset
            else if (pins == 10)
            {
                bowl += 1;
                return Action.Reset;
            } // If on roll 1 or 2 without a strike, tidy
            else if (firstRoll)
            {
                firstScore = pins;
                firstRoll = false;
                bowl += 1;
                return Action.Tidy;
            } // If spare on roll 2, reset
            else if (firstScore + pins == 10)
            {
                bowl += 1;
                firstScore = 0; // reset first score
                firstRoll = true;
                return Action.Reset;
            } // If open on roll 2, end game
            else
            {
                bowl = 0;
                return Action.EndGame;
            }
        }

        // First Roll of Frames 1 - 9
        if (bowl % 2 == 0)
        {
            // The player bowls a strike, move the player bowl index up 2 and end turn
            if (pins == 10)
            {
                bowl += 2;
                return Action.EndTurn;
            }
            else // The player doesn't bowl a strike, increment the bowl index 1 and tidy
            {
                bowl += 1;
                return Action.Tidy;
            }
        }
        else // Second roll, increment the bowl index 1 and end turn
        {
            bowl += 1;
            return Action.EndTurn;
        }

        // throw new UnityException("Not sure what action to return!");
    }
}
