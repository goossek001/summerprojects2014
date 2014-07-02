using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class HeliFlyControls : MonoBehaviour {
	float maxUpwardsForce;
	float maxDownwardsForce;

	float rotationBackForce;

	// Use this for initialization
	void Start () {
		maxUpwardsForce = 15;
		maxDownwardsForce = 4;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Update lift
		float lift = Input.GetAxis("Lift");
		float verticalForce;
		if (lift == 0) {
			verticalForce = -Physics.gravity.y;
		} else if (lift > 0) {
			verticalForce = lift * maxUpwardsForce;
		} else {
			verticalForce = lift * maxDownwardsForce;
		}
		verticalForce *= rigidbody.mass;
		Vector3 force = new Vector3 (0, verticalForce, 0);
		rigidbody.AddRelativeForce(force);

		//Update pitch and roll
		float currentPitch = rigidbody.rotation.eulerAngles.x;
		if (currentPitch > 180) currentPitch = -(currentPitch - 360);
		float currentRoll = rigidbody.rotation.eulerAngles.z;
		if (currentRoll > 180) currentRoll = (currentRoll - 360);
		//Rotate back toward no pitch and roll
		rigidbody.AddRelativeTorque(0.001f*currentPitch, 0, 0.001f*-currentRoll);

		float pitchInput = Mathf.Clamp(Input.GetAxis("Pitch"), -1, 1);
		float rollInput = Mathf.Clamp(Input.GetAxis("Roll"), -1, 1);
		rigidbody.AddRelativeTorque(0.2f*pitchInput, 0, 0.2f*rollInput);

		//Update yaw
		float yawInput = Input.GetAxis("Yaw");
		rigidbody.AddRelativeTorque(0, 0.3f*yawInput, 0);

	}
}
