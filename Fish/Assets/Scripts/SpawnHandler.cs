using UnityEngine;
using System.Collections;

public class SpawnHandler: MonoBehaviour {
	public float spawnRate = 0.75f;
	public float maxNumberOfFish = 12;
	private int numberOfFish;

	private GameObject fishPrefab;
	private GameObject player;

	private float[] fishSizes;

	// Use this for initialization
	public void Start () {
		numberOfFish = 1;

		fishPrefab = (GameObject) Resources.Load("Fish", typeof (GameObject));

		fishSizes = new float[12];
		fishSizes [0] = 0.7f;
		fishSizes [1] = 1;
		for (int i = 2; i < fishSizes.Length; i++) {
			fishSizes [i] = 1 + 0.5f*Mathf.Pow (fishSizes [i-1], 1.4f);
		}

		for (int i = 0; i < numberOfFish/4; i++) {
			SpawnFish();
        }
	}

	public void SetPlayer(GameObject playerObject) {
		player = playerObject;
	}
	
	// Update is called once per frame
	public void Update () {
		if ((int)numberOfFish < maxNumberOfFish && spawnRate * Time.deltaTime > Random.value) {
			SpawnFish();
		}
	}

	public int PlayerSizeIndex() {
		int i = 0;

		while (i+1 < fishSizes.Length && Mathf.Abs (fishSizes[i]-player.transform.localScale.y) > Mathf.Abs (fishSizes[i+1]-player.transform.localScale.y)) {
			i++;
		}

		return i;
	}

	public void SpawnFish() {
		numberOfFish++;
		int direction = Random.value < 0.5f ? -1 : 1;
		Vector2 position = (new Vector2 (transform.position.x + direction * (collider2D.bounds.max.x-1), (Random.value*2-1)*(collider2D.bounds.max.y - 10)));
		int rotation = direction == -1? 0: 180;
		GameObject fish = (GameObject) Instantiate(fishPrefab, (Vector3) position, Quaternion.Euler(0, 0, rotation));

		int playerSizeIndex = PlayerSizeIndex ();
		float randomValue = Random.value;
		int sizeIndex;
		if (randomValue > 0.95 && playerSizeIndex + 3 < fishSizes.Length) {
				sizeIndex = playerSizeIndex + 3;
		} else if (randomValue > 0.85 && playerSizeIndex + 2 < fishSizes.Length) {
				sizeIndex = playerSizeIndex + 2;
		} else if (randomValue > 0.7 && playerSizeIndex + 1 < fishSizes.Length) {
				sizeIndex = playerSizeIndex + 1;
		} else {
			sizeIndex = (int) (Random.value * Mathf.Min(fishSizes.Length, playerSizeIndex));
		}
		fish.GetComponent<Biting>().Growh(fishSizes[sizeIndex]-1);
	}

	public void OnTriggerExit2D(Collider2D leavingObject) {
		if (leavingObject.transform.parent == null) {
			if (leavingObject.gameObject.GetComponent<FishAI>() != null) {
				numberOfFish--;
			}

			Destroy (leavingObject.gameObject);
		}
	}
}
