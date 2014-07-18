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
		if (other != null) {
			if (transform.localScale.y > other.transform.localScale.y) {
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
		BloodEffectGenerator bloodEffect = other.GetComponentInChildren<BloodEffectGenerator> ();
		if (other != null) bloodEffect.Activate();

		Destroy (other);
		spawn.AFishDied ();

		mouthOpener.CloseMouth ();
	}
}
