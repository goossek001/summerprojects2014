using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
//This script make a object fly like a helicopter, with input or see other players over a network
public class ChopperFlyPhysics : Photon.MonoBehaviour {
	//Variables that divine how much lift and torque power the helicopter has
	public float mainBladeMaxForce = 20000;			//This variable determen the max lift force
	public float mainBladeMaxTorque = 15000;		//This variable determen the max torque for pitch and roll
	public float rotorBladeMaxForce = 30000;		//This variable determen the max torque for yaw

	//Varaibles used to lerp objects position and rotations between the updates of the server
	private Vector3 realPosition;
	private Quaternion realRotation;

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

	//This method is called every frame and lerp objects position and rotations between the updates of the server
	public void Update () {
		if (photonView.isMine) {
			//DO NOTHING FOR MY OWN OBJECT - because the update method that listen to input is inside chopper input
		} else {
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
	}

	//Write position and rotation to server for own object and receive them for objects of other players
	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		bool isMyObject = stream.isWriting;

		//Write position and transform to a server for own object
		if (isMyObject) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		//Receive position and rotation for objects of other players
		} else {
			realPosition = (Vector3) stream.ReceiveNext();
			realRotation = (Quaternion) stream.ReceiveNext();
		}
	}
}