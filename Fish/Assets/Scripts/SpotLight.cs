using UnityEngine;
using System.Collections;

public class SpotLight : MonoBehaviour {
	private GameObject player;

	// Update is called once per frame
	void FixedUpdate () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			while (player.transform.parent != null && player.transform.parent.tag == "Player") {
				player = player.transform.parent.gameObject;
			}
		}
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);;
	}
}
