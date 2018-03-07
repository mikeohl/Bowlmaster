/* LaneBox detects if the ball exits the lane box and calls
 * the game manager to set it's out of play flag 
 */

using UnityEngine;

public class LaneBox : MonoBehaviour {

    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Set gameManager out of play flag on ball exit from lane box
    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.name == "Ball") {
            gameManager.SetBallOutOfPlay(true);
        }
    }
}
