using UnityEngine;

public class LaneBox : MonoBehaviour {

    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.name == "Ball") {
            gameManager.SetBallOutOfPlay(true);
        }
    }
}
