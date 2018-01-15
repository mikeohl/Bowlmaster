using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinSetter : MonoBehaviour {

    // public Pin [] pins;
    public GameObject pins;
    public float pinRaiseHeight = 40.0f;

    private Animator animator;
    
    // Use this for initialization
    void Start () {
        animator = GameObject.FindObjectOfType<Animator>();
    }

    public void SetPins (ActionMaster.Action action)
    {
        // Update Bowl Score
        Debug.Log(action);
        switch (action)
        {
            case ActionMaster.Action.Tidy:
                animator.SetTrigger("tidyTrigger");
                break;

            case ActionMaster.Action.EndGame:
            case ActionMaster.Action.EndTurn:
            case ActionMaster.Action.Reset:
                animator.SetTrigger("resetTrigger");
                break;
        }
    }

    // Raise Pins to avoid swiper
    public void RaisePins ()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                pin.transform.Translate(new Vector3(0.0f, pinRaiseHeight, 0.0f), Space.World);
                pin.DisableGravity();
                pin.Stabilize();
            }
        }
    }

    // Lower pins back down to the lane
    public void LowerPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.transform.Translate(new Vector3(0.0f, -pinRaiseHeight, 0.0f), Space.World);
            pin.EnableGravity();
            pin.Stabilize();
        }
    }

    // Create a new set of 10 pins to be placed on the lane
    public void RenewPins ()
    {
        Instantiate(pins);
        RaisePins();
    }

    // Eliminate Pins that leave the Pin Setter box
    public void OnTriggerExit(Collider collider)
    {
        // Attempt to get Pin object from collider
        Pin pin = collider.gameObject.GetComponentInParent<Pin>();

        // If colliding object was pin, destroy child objects and then the pin itself.
        if (pin)
        {
            foreach (Transform child in pin.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pin.gameObject);
        }
    }
}
