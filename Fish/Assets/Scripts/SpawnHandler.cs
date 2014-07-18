using UnityEngine;
using System.Collections;

public class SpawnHandler: MonoBehaviour {
	private float requiredNumberOfFish;
	private int numberOfFish;

	public GameObject fishPrefab;

	// Use this for initialization
	public void Start () {
		requiredNumberOfFish = 10;
		numberOfFish = 1;
	}
	
	// Update is called once per frame
	public void Update () {
		while ((int)numberOfFish < requiredNumberOfFish) {
			SpawnFish();
		}
	}

	public void SpawnFish() {
		numberOfFish++;
		int direction = Random.value < 0.5f ? -1 : 1;
		Vector2 position = (new Vector2 (transform.position.x + direction * (collider2D.bounds.max.x-1), (Random.value*2-1)*(collider2D.bounds.max.y - 10)));
		int rotation = direction == -1? 0: 180;
		GameObject fish = (GameObject) Instantiate(fishPrefab, (Vector3) position, Quaternion.Euler(0, 0, rotation));

		float size = 0.9f;
		fish.GetComponent<Biting>().Growh(size-1);
	}

	public void AFishDied() {
		numberOfFish--;
	}

	public void OnTriggerExit2D(Collider2D leavingObject) {
		if (leavingObject.transform.parent == null) {
			if (leavingObject.gameObject.GetComponent<FishAI>() != null) {
				AFishDied();
			}

			Destroy (leavingObject.gameObject);
		}
	}
}
