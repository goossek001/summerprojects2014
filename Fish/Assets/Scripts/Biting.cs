using UnityEngine;
using System.Collections;

public class Biting : MonoBehaviour {

	private SpawnHandler spawn;
	private MouthOpening mouthOpener;

	public void Start () {
		spawn = Camera.main.GetComponentInChildren<SpawnHandler> ();
		mouthOpener = GetComponentInChildren<MouthOpening> ();
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		Transform other = collision.transform;
		while (other != null && other.tag != "Fish") {
			other = other.transform.parent;
		}
		if (other != null && IsInBiteRange(other.gameObject)) {
			if (transform.localScale.y >= other.transform.localScale.y) {
				Eat(other.gameObject);
			} else {
				FishAI ai = other.GetComponent<FishAI>();
				if (ai != null) {
					ai.FishLocated(gameObject);
				}
			}
		}
	}

	private void Eat(GameObject other) {
		CreateBlood (other);
		Destroy (other);
		spawn.AFishDied ();

		mouthOpener.CloseMouth ();
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
