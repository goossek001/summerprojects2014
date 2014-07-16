using UnityEngine;
using System.Collections;

public class Biting : MonoBehaviour {

	private SpawnHandler spawn;

	public void Start () {
		spawn = Camera.main.GetComponentInChildren<SpawnHandler> ();
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		GameObject other = collision.gameObject;
		while (other.tag != "Fish" && other != null) {
			other = other.transform.parent.gameObject;
		}
		if (other != null && IsInBiteRange(other)) {
			if (transform.localScale.y >= other.transform.localScale.y) {
				Eat(other);
			} else {
				FishAI ai = other.GetComponent<FishAI>();
				if (ai != null) {
					ai.Poke(gameObject);
				}
			}
		}
	}

	private void Eat(GameObject other) {
		CreateBlood (other);
		Destroy (other);
		spawn.AFishDied ();
	}

	private void CreateBlood(GameObject eatenFish) {

	}

	private bool IsInBiteRange(GameObject other) {
		float maxAngle = 90;
		
		Vector2 difference = other.transform.position - transform.position;
		float angle = 90-Mathf.Atan2 (difference.y, difference.x)*180/Mathf.PI - transform.eulerAngles.z;

		return angle <= maxAngle || angle >= 360 - maxAngle;
	}
}
