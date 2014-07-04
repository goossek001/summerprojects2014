using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class HeliFlyControls : MonoBehaviour {
	float maxUpwardsForce;
	float maxDownwardsForce;

	float rotationBackForce;

	// Use this for initialization
	public void Start () {
		maxUpwardsForce = 25;
		maxDownwardsForce = 11;
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		UpdateLift();
		UpdateRotation();
	}

	//Add force in the vertical direction of the chopper
	private void UpdateLift() {
		float lift = Input.GetAxis("Lift");
		float verticalForce;
		if (lift == 0) {
			verticalForce = -Physics.gravity.y;
		} else if (lift > 0) {
			verticalForce = lift * maxUpwardsForce * rigidbody.mass;
		} else {
			verticalForce = lift * maxDownwardsForce * rigidbody.mass;
		}

		Vector3 force = new Vector3 (0, verticalForce, 0);
		rigidbody.AddRelativeForce(force);
	}

	//Update rotation in all 3 directions
	private void UpdateRotation() {
		//Slowly rotate back to 0 roll and pitch
		float currentPitch = rigidbody.rotation.eulerAngles.x;
		if (currentPitch > 180) currentPitch -= 360;
		float currentRoll = rigidbody.rotation.eulerAngles.z;
		if (currentRoll > 180) currentRoll -= 360;
		rigidbody.AddRelativeTorque(0.01f*-currentPitch*rigidbody.mass, 0, 0.01f*-currentRoll*rigidbody.mass);

		//Roll and pitch
		float pitchInput = Mathf.Clamp(Input.GetAxis("Pitch"), -1, 1);
		float rollInput = Mathf.Clamp(Input.GetAxis("Roll"), -1, 1);
		rigidbody.AddRelativeTorque(4f*pitchInput*rigidbody.mass, 0, 4f*rollInput*rigidbody.mass);

		//Update yaw
		float yawInput = Input.GetAxis("Yaw");
		rigidbody.AddRelativeTorque(0, 6f*yawInput*rigidbody.mass, 0);
	}
}
