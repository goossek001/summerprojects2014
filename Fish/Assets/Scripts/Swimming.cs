using UnityEngine;
using System.Collections;

//Make a fish tail move and give with this give the fish force and give the fish torque as result of water moving against the tail when the fish is moving 
abstract public class Swimming : MonoBehaviour {

	//The fish body parts that move seperately
	public GameObject body;
	public GameObject[] tail;

	//The swimming propeties
	public float turnPower = 20;
	public float swimPower = 8;

	private HingeJoint2D[] joints;

	virtual public void Start () {
		joints = new HingeJoint2D[tail.Length];

		joints[0] = body.GetComponent<HingeJoint2D>();
		for (int i = 1; i < joints.Length; i++) {
			joints[i] = tail[i-1].GetComponent<HingeJoint2D>();
		}
	}

	protected void Swim (float input) {
		float torque = input * turnPower;
		for (int i = 0; i < tail.Length; i++) {
			if (isJointFullyTurned(joints[i])) {
				tail[i].rigidbody2D.AddTorque(torque);
			}
		}
		body.rigidbody2D.AddTorque(-torque*tail.Length*1.75f);

		if (isJointFullyTurned(joints[0])) {
			float power = (body.rigidbody2D.velocity - tail [tail.Length - 1].rigidbody2D.velocity).magnitude * swimPower;
			body.rigidbody2D.AddRelativeForce(new Vector2(power, 0));
		}
	}

	private static bool isJointFullyTurned(HingeJoint2D joint) {
		return joint.jointAngle * 0.9f < joint.limits.max && joint.jointAngle * 0.9f > joint.limits.min;
	}
}
