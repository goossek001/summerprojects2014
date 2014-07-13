using UnityEngine;
using System.Collections;

public class PlayerInput : Swimming {

	public void FixedUpdate () {
		float input = Input.GetAxis ("Horizontal");
		if (input != 0) {
			Swim(input);
		}
	}
}
