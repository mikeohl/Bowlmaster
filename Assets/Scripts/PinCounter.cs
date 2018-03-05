using UnityEngine;

public class PinCounter : MonoBehaviour {

    // Count the number of upright pins
    public int CountStandingPins() {
        // Assume NO pins are standing
        int standingPins = 0;

        // Loop through pins, check if standing and update count
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standingPins++;
            }
        }
        return standingPins;
    }
}
