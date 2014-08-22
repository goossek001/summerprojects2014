using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class Boat : MonoBehaviour {

	public float acceleration = 10;

	public void FixedUpdate () {
		rigidbody2D.AddForce (new Vector2 (acceleration * rigidbody2D.mass * Time.fixedDeltaTime, 0));
	}
}
