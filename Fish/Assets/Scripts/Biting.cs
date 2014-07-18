using UnityEngine;
using System.Collections;

public class Biting : MonoBehaviour {

	private SpawnHandler spawn;
	private MouthOpening mouthOpener;

	public float growhRate = 0.1f;

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
					ai.Poke(gameObject);
				}
			}
		}
	}

	private void Eat(GameObject food) {
		BloodEffectGenerator bloodEffect = food.GetComponentInChildren<BloodEffectGenerator> ();
		if (food != null) bloodEffect.Activate();

		mouthOpener.RemoveFish (food);
		Growh (food);
		Destroy (food);
		
		spawn.AFishDied ();
	}

	private void Growh (GameObject food) {
		Vector2 localScale = transform.localScale;
		localScale.x = localScale.y = localScale.y + growhRate * food.transform.localScale.y / localScale.y;
		transform.localScale = localScale;
	}
}
