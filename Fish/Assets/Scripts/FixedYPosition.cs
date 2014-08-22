using UnityEngine;
using System.Collections;

public class FixedYPosition : MonoBehaviour {
	
	private float position_y;
	
	public void Start() {
		position_y = transform.position.y;
	}

	public void FixedUpdate () {
		if (transform.position.y != position_y) {
			transform.position = (Vector3) new Vector2(transform.position.x, position_y);
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
		}
	
	}
}
