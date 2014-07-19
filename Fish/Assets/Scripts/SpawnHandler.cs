using UnityEngine;
using System.Collections;

public class SpawnHandler: MonoBehaviour {
	public float spawnRate = 1;
	public float maxNumberOfFish = 12;
	private int numberOfFish;

	private GameObject fishPrefab;
	private GameObject player;

	private float[] fishSizes;

	// Use this for initialization
	public void Start () {
		numberOfFish = 1;

		fishPrefab = (GameObject) Resources.Load("Fish", typeof (GameObject));

		fishSizes = new float[6];
		fishSizes [0] = 0.7f;
		fishSizes [1] = 1;
		for (int i = 2; i < fishSizes.Length; i++) {
			fishSizes [i] = 1 + Mathf.Pow (fishSizes [i-1], 1.1f);
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
		
		//!!!Warning - Temp code, should be replaced with a calculation that will less bigger fish spawn when the player is small
		int playerSizeIndex = PlayerSizeIndex ();
		int sizeIndex = (int) (Random.value * fishSizes.Length);
		fish.GetComponent<Biting>().Growh(fishSizes[0]-1);
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
