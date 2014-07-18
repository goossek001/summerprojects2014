using UnityEngine;
using System.Collections;

//Make a fish tail move and give with this give the fish force and give the fish torque as result of water moving against the tail when the fish is moving 
abstract public class Swimming : MonoBehaviour {

	//The fish body parts that move seperately
	public GameObject body;
	public GameObject[] tail;

	//The swimming propeties
	public float turnPower = 10;
	public float tailSwinPower = 0.2f;
	public float swimPower = 0.04f;

	private HingeJoint2D[] joints;

	virtual public void Start () {
		joints = new HingeJoint2D[tail.Length];

		joints[0] = body.GetComponent<HingeJoint2D>();
		for (int i = 1; i < joints.Length; i++) {
			joints[i] = tail[i-1].GetComponent<HingeJoint2D>();
		}

		turnPower *= Mathf.Pow (transform.localScale.y, 3);
		tailSwinPower *= Mathf.Pow (transform.localScale.y, 3);
		swimPower *= Mathf.Pow (transform.localScale.y, 2.4f);

		rigidbody2D.mass *= Mathf.Pow (transform.localScale.y, 2);
		for (int i = 0; i < transform.childCount; i++) {
			Rigidbody2D rigidbody = transform.GetChild(i).rigidbody2D;
			if (rigidbody != null) {
				rigidbody.mass *= Mathf.Pow (transform.localScale.y, 2);
			}
		}
	}

	protected void Swim (float input) {
		try {
			float tailSwing = input * tailSwinPower;
			for (int i = 0; i < tail.Length; i++) {
				tail [i].rigidbody2D.AddTorque (tailSwing);
			}
			body.rigidbody2D.AddTorque (-(tailSwing*tailSwinPower * tail.Length + input * turnPower));

			float swimPower = 0;
			for (int i = 0; i < joints.Length; i++) {
				swimPower += Mathf.Abs(joints [i].jointSpeed);
			}
			if (swimPower != 0) {
				swimPower *= this.swimPower;
				body.rigidbody2D.AddRelativeForce (new Vector2 (swimPower, 0));
			}
		} catch (System.Exception) {
			//The fish is destroyed
		}
		
	}

	private static bool isJointFullyTurned (HingeJoint2D joint, int dirrection) {
		bool isFullyTurned;
		if (dirrection > 0) {
			isFullyTurned = joint.jointAngle >= joint.limits.max*0.96f;
			if (joint.jointAngle > joint.limits.max) {
				//joint.jointAngle = joint.limits.max;
			}
		} else {
			isFullyTurned = joint.jointAngle <= joint.limits.min*0.96f;
			if (joint.jointAngle < joint.limits.min) {
				//joint.jointAngle = joint.limits.max;
			}
		}
		return isFullyTurned;
	}
}
