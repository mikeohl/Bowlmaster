/* PinCounter contains method to return number of standing pins */

using UnityEngine;

public class PinCounter : MonoBehaviour {

    // Count the number of upright pins
    public int CountStandingPins() {

        int standingPins = 0; // Assume NO pins are standing

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standingPins++;
            }
        }
        return standingPins;
    }
}
