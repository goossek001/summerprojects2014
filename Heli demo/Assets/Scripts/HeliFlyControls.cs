using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
//This script make a object fly like a helicopter, with input the object can get increased velocity to the vertical direction of the object and it can rotate over all 3 axes
public class HeliFlyControls : MonoBehaviour {
	//Variables that divine how much lift and torque power the helicopter has
	public float mainBladeMaxForce = 20000;
	public float mainBladeMaxTorque = 15000;
	public float rotorBladeMaxForce = 30000;
	
	// Listen to input to add force or torque
	public void FixedUpdate () {
		//Update lift and trust
		float liftInput = Input.GetAxis("Lift");
		Vector3 relativeForce = new Vector3 (0, -Physics.gravity.y*rigidbody.mass + liftInput * mainBladeMaxForce, 0);
		rigidbody.AddRelativeForce(relativeForce);

		//Update roll and pitch
		float pitchInput = Mathf.Clamp(Input.GetAxis("Pitch"), -1, 1);
		float rollInput = Mathf.Clamp(Input.GetAxis("Roll"), -1, 1);
		rigidbody.AddRelativeTorque(mainBladeMaxTorque*pitchInput, 0, mainBladeMaxTorque*rollInput);

		//Update yaw
		float yawInput = Input.GetAxis("Yaw");
		rigidbody.AddRelativeTorque(0, rotorBladeMaxForce*yawInput, 0);
	}
}
