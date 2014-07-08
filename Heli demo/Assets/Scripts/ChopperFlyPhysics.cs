using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
//This script make a object fly like a helicopter, with input or see other players over a network
public class ChopperFlyPhysics : MonoBehaviour {
	//Variables that divine how much lift and torque power the helicopter has
	public float mainBladeMaxForce = 20000;			//This variable determen the max lift force
	public float mainBladeMaxTorque = 15000;		//This variable determen the max torque for pitch and roll
	public float rotorBladeMaxForce = 30000;		//This variable determen the max torque for yaw

	//Add force in the vertical direction, relative to the chopper
	public void AddLift (float input) {
		Vector3 relativeForce = new Vector3 (0, -Physics.gravity.y*rigidbody.mass + input * mainBladeMaxForce, 0);
		rigidbody.AddRelativeForce(relativeForce);
	}

	//Add torque to the chopper
	public void AddTorque (float pitchInput, float rollInput, float yawInput) {
		float pitch = mainBladeMaxTorque * pitchInput;
		float roll = mainBladeMaxTorque * rollInput;
		float yaw = rotorBladeMaxForce * yawInput;

		rigidbody.AddRelativeTorque(pitch, yaw, roll);
	}
}