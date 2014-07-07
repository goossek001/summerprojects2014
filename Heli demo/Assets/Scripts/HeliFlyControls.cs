using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class HeliFlyControls : MonoBehaviour {
	public float maxLiftForce;

	public float maxMainBladeTorcForce;
	public float maxRotorBladeForce;

	// Use this for initialization
	public void Start () { }
	
	// Update is called once per frame
	public void FixedUpdate () {
		//Update lift and trust
		float liftInput = Input.GetAxis("Lift");
		Vector3 relativeForce = new Vector3 (0, -Physics.gravity.y*rigidbody.mass + liftInput * maxLiftForce, 0);
		rigidbody.AddRelativeForce(relativeForce);

		//Update roll and pitch
		float pitchInput = Mathf.Clamp(Input.GetAxis("Pitch"), -1, 1);
		float rollInput = Mathf.Clamp(Input.GetAxis("Roll"), -1, 1);
		rigidbody.AddRelativeTorque(maxMainBladeTorcForce*pitchInput, 0, maxMainBladeTorcForce*rollInput);

		//Update yaw
		float yawInput = Input.GetAxis("Yaw");
		rigidbody.AddRelativeTorque(0, maxRotorBladeForce*yawInput, 0);
	}
}
