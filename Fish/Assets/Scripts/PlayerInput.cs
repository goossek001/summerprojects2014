using UnityEngine;
using System.Collections;

public class PlayerInput : Swimming {

	public override void Start() {
		base.Start ();
		GetComponent<Biting> ().Growh (10);

		Camera.main.GetComponentInChildren<SpawnHandler> ().SetPlayer (gameObject);
	}

	public void FixedUpdate () {
		float input = Input.GetAxis ("Horizontal");
		if (input != 0) {
			Swim(input);
		}
	}
}
