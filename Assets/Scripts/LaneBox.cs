using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            gameManager.SetBallOutOfPlay(true);
        }
    }
}
