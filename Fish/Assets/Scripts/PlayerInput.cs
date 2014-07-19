using UnityEngine;
using System.Collections;

public class PlayerInput : Swimming {

	public override void Start() {
		base.Start ();
	}

	public void FixedUpdate () {
		float input = Input.GetAxis ("Horizontal");
		if (input != 0) {
			Swim(input);
		}
		//GetComponent<Biting> ().Growh (0.1f*Time.fixedDeltaTime);
	}
}
