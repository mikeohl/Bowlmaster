/* Pin contains methods to check if pin is standing (has it been knocked down?)
 * and to stabilize the pin to zero motion once it is set. Also includes 
 * helper gravity functions for use with pin setter animation.
 */

using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3.0f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start () {
        GetRigidBody();
        Debug.Assert(rigidBody);
    }

    // Check if pin orientation is sufficient for a standing pin
    public bool IsStanding () {
        Vector3 eulerRotation = transform.rotation.eulerAngles;

        float xTilt = ((eulerRotation.x + standingThreshold) % 360.0f) / 2.0f;
        float zTilt = ((eulerRotation.z + standingThreshold) % 360.0f) / 2.0f;

        if (xTilt < standingThreshold && zTilt < standingThreshold) {
            return true;
        } else {
            return false;
        }
    }

    // Set pin attributes to stop pin from moving
    public void Stabilize () {
        if (!rigidBody) {
            GetRigidBody();
        }
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    // For use with pin setter animation
    public void EnableGravity () {
        if (!rigidBody) { GetRigidBody(); }
        rigidBody.useGravity = true;
    }

    // For use with pin setter animation
    public void DisableGravity () {
        if (!rigidBody) { GetRigidBody(); }
        rigidBody.useGravity = false;
    }

    private void GetRigidBody () {
        rigidBody = GetComponentInChildren<Rigidbody>();
    }
}
