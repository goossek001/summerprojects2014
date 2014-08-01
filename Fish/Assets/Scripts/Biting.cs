using UnityEngine;
using System.Collections;

public class Biting : MonoBehaviour {

	private Swimming movementScript;

	public HingeJoint2D[] joints;

	public float growhRate = 0.1f;
	private float progress;
	private ProgressBar progressBar;

	public virtual void Start () {
		progress = 0;

		progressBar = GetComponent<ProgressBar> ();
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		Transform other = collision.transform;
		while (other != null && (other.tag != "Fish" && other.tag != "Player")) {
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

	protected virtual void Eat(GameObject food) {
		BloodEffect bloodEffect = food.GetComponentInChildren<BloodEffect> ();
		if (bloodEffect != null) bloodEffect.Activate();
		
		if (food.tag == "Player") {
			//Game Over
			//!!!Missing code to let player know that he died and missing code to restart the game
		}

		Growh (food);

		Destroy (food);
	}
	
	private void Growh (GameObject food) {
		float deltaSize = growhRate * food.transform.localScale.y / transform.localScale.y;
		progress += deltaSize;

		if (progress >= 1) {
			progress = 0;
			progressBar.SetSize (progress);

			char[] seperators = {'('};
			string prefabName = name.Split(seperators)[0];
			GameObject prefab = (GameObject) Resources.Load(prefabName, typeof (GameObject));
			
			GameObject newFish = (GameObject) Instantiate(prefab, transform.position, transform.rotation);
			newFish.GetComponent<Biting>().Growh (0.5f*Mathf.Pow(transform.localScale.y, 1.4f));
			newFish.rigidbody2D.velocity = rigidbody2D.velocity;
			for (int i = 0; i < newFish.transform.childCount; i++) {
				Transform newChild = newFish.transform.GetChild(i);
				Transform oldChild = transform.GetChild(i);
				if (newChild.rigidbody2D != null) {
					oldChild = transform.GetChild(i);
					if (oldChild.name != newChild.name) {
						for (int j = 0; j < transform.childCount; j++) {
							oldChild = transform.GetChild(j);
							if (oldChild.name == newChild.name) {
								break;
							}
						}
					}
					newChild.rigidbody2D.velocity = oldChild.rigidbody2D.velocity;
				}
			}

			Destroy (gameObject);
		} else {
			if (progressBar != null) progressBar.SetSize (progress);
		}
	}

	//!!!Warning - Only use on init (when joints have a angle other that 0 this method will break the joint)
	public void Growh (float deltaSize) {
		if (movementScript == null) {
			movementScript = GetComponent<Swimming> ();
		}

		Vector2 localScale = transform.localScale;
		localScale.x += deltaSize;
		localScale.y += deltaSize;
		transform.localScale = localScale;
		
		rigidbody2D.mass += Mathf.Sign(deltaSize) * rigidbody2D.mass * Mathf.Pow (deltaSize, 2);
		for (int i = 0; i < transform.childCount; i++) {
			Rigidbody2D childRigidbody = transform.GetChild(i).rigidbody2D;
			if (childRigidbody != null) {
				childRigidbody.mass += Mathf.Sign(deltaSize) * childRigidbody.mass * Mathf.Pow (deltaSize, 2);
			}
		}

		movementScript.GrowhPower (deltaSize);
	}
}
