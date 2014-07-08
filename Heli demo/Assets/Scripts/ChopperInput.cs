using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ChopperFlyPhysics))]
//This script listen to input to fly the chopper
public class ChopperInput : MonoBehaviour {
	private GameObject standByCamera;			//The camera that will be active if there is no other camera avaible
	private GameObject firstPersonCamera;		//The camera that will be active when you fly a chopper

	private ChopperFlyPhysics chopperPhysics;	//The object used to add force and torque to the chopper

	//This method is called at init of the object and change camera view
	public void Start () {
		//Find object and scripts
		standByCamera = GameObject.Find ("StandByCamera") as GameObject;
		firstPersonCamera = transform.Find ("FirstPersonCamera").gameObject;
		chopperPhysics = GetComponent<ChopperFlyPhysics> ();

		//Replace the view, that is now the standby camera, with the camera on the chopper
		standByCamera.SetActive (false);
		firstPersonCamera.SetActive (true);
	}

	//This method is called every physics update and it listen to input to add force or torque to the chopper
	public void FixedUpdate () {
		//Add lift and trust
		float liftInput = Input.GetAxis("Lift");
		chopperPhysics.AddLift (liftInput);

		//Add torque
		float pitchInput = Mathf.Clamp(Input.GetAxis ("Pitch"), -1, 1);	//Clamp is used, because mouse input can exceed 1 and -1
		float rollInput = Mathf.Clamp(Input.GetAxis ("Roll"), -1, 1);	//Clamp is used, because mouse input can exceed 1 and -1
		float yawInput = Input.GetAxis ("Yaw");
		chopperPhysics.AddTorque (pitchInput, rollInput, yawInput);
	}

	//This method is called at destruction of the object and it change the camera view
	public void OnDestroy () {
		//Replace the view, that is now the camera on the chopper, with the standby camera
		firstPersonCamera.SetActive (false);
		standByCamera.SetActive (true);
	}
}
