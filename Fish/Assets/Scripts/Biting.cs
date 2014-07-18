using UnityEngine;
using System.Collections;

public class Biting : MonoBehaviour {

	private SpawnHandler spawn;
	private MouthOpening mouthOpener;
	private Swimming movementScript;

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
		float deltaSize = growhRate * food.transform.localScale.y / transform.localScale.y;
		Growh (deltaSize);
	}

	public void Growh (float deltaSize) {
		if (movementScript == null) {
			movementScript = GetComponent<Swimming> ();
		}

		Vector2 localScale = transform.localScale;
		localScale.x += deltaSize;
		localScale.y += deltaSize;
		transform.localScale = localScale;
		
		rigidbody2D.mass += rigidbody2D.mass * Mathf.Pow (deltaSize, 2);
		for (int i = 0; i < transform.childCount; i++) {
			Rigidbody2D childRigidbody = transform.GetChild(i).rigidbody2D;
			if (childRigidbody != null) {
				childRigidbody.mass += childRigidbody.mass * Mathf.Pow (deltaSize, 2);
			}
		}

		movementScript.GrowhPower (deltaSize);
	}
}
