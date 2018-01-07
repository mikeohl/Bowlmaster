using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    // public Pin [] pins;
    public Text standingDisplay;

    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();
	}

    public int CountStanding ()
    {
        // Assume all pins are standing
        int standingPins = 10;

        // Loop through pins, check if standing and update count
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (!pin.IsStanding())
            {
                standingPins--;
            }
        }
        return standingPins;
    }

    public void OnTriggerEnter (Collider collider)
    {
        GameObject triggeredObject = collider.gameObject;

        // Update color if object collided is ball
        if (triggeredObject.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            UpdateColor();
        }  
    }

    public void OnTriggerExit(Collider collider)
    {
        Pin pin = collider.gameObject.GetComponentInParent<Pin>();
        if (pin)
        {
            foreach (Transform child in pin.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pin.gameObject);
        } else
        {
            Destroy(collider.gameObject);
        }
        
        //print(collider.gameObject.GetComponentInParent<Pin>());
        //Destroy(collider.gameObject.GetComponentInParent<Pin>());
    }

    public void UpdateColor ()
    {
        standingDisplay.color = Color.red;
    }
}
