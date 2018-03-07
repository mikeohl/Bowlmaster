/* ActionMaster controls the action that takes place after a roll.
 * Actions are listed in enum as Tidy (sweep pins), Reset (set new pins),
 * EndTurn, and EndGame
 */

using System.Collections.Generic;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame, _Undefined};

    // Given list of pins knocked down on each roll, determine next 
    // action that should be taken by the actionMaster
    public static Action NextAction (List<int> roll) {
        ActionMaster actionMaster = new ActionMaster();
        Action nextAction = Action._Undefined;
        int bowl = 1; // count all potential rolls, roll list only contains actual rolls
        int firstScore = 0;
        bool firstRoll = true;

        for(int i = 0; i < roll.Count; i++, bowl++) {

            if (bowl > 18) { // Handle special case for Frame 10
                if (bowl == 21) {
                    nextAction = Action.EndGame;
                } else if (roll[i] == 10) {              // Strikes
                    nextAction = Action.Reset;
                } else if (firstRoll) { 
                    firstScore = roll[i];
                    firstRoll = false;
                    nextAction = Action.Tidy;
                } else if (firstScore + roll[i] == 10) { // Spare
                    nextAction = Action.Reset;
                } else {                                 // Open frame
                    nextAction = Action.EndGame;
                }
            } else if (bowl % 2 == 1) { // On first roll of frame 1 - 9
                if (roll[i] == 10) {
                    bowl++;
                    nextAction = Action.EndTurn;
                } else {
                    nextAction = Action.Tidy;
                }
            } else { // On second roll
                nextAction = Action.EndTurn;
            }
        }
        return nextAction;
    }
}
