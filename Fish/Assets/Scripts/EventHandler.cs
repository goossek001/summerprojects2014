using UnityEngine;
using System.Collections;

//This class manage the timing of evemt (The events are: spawning a fishing boat with net or with hook and spawning a sidder ale)
public class EventHandler : MonoBehaviour {
	public float avarageEventsOccurence = 60;

	public float netEventChance = 0.4f;
	public float hookChance = 0.4f;
	public float sidderAleChance = 0.2f;

	public GameObject netBoatPrefab;
	public GameObject hookBoatPrefab;
	public GameObject sidderAle;

	private GameObject eventObject;

	public void Start () {
		eventObject = null;
	}

	public void Update() {
		if (eventObject == null && Random.value < Time.deltaTime / avarageEventsOccurence) {
			float randomValue = Random.value * (netEventChance + hookChance + sidderAleChance);
			char moveDirection = Random.value < 0.5f? 'r': 'l';

			if (randomValue < netEventChance) {
				eventObject = StartNetEvent(moveDirection);
			} else if (randomValue < netEventChance + hookChance) {
				eventObject = StartHookEvent(moveDirection);
			} else {
				eventObject = StartSidderAleEvent(moveDirection);
			}
		}
	}

	public GameObject StartNetEvent (char moveDirection) {
		Vector2 position = new Vector2 ((moveDirection == 'r'? -75: 75), 19.5f);
		GameObject boat = (GameObject) Instantiate (netBoatPrefab, position, Quaternion.identity);
		if (moveDirection == 'l') {
			Vector3 scale = boat.transform.localScale;
			scale.x *= -1;
			boat.transform.localScale = scale;
		}

		return boat;
	}
	
	public GameObject StartHookEvent (char moveDirection) {
		return null;
	}
	
	public GameObject StartSidderAleEvent (char moveDirection) {
		return null;
	}
}
