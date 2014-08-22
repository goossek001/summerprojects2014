using UnityEngine;
using System.Collections;

public class ChainSpawn: MonoBehaviour {

	public GameObject chainShaklePrefab;

	public GameObject net;

	public void Start () {
		Vector2 start = (Vector2) net.transform.position + net.GetComponent<HingeJoint2D> ().anchor;
		Vector2 end = (Vector2) transform.position - 0.5f * (Vector2) renderer.bounds.size;

		SpawnChain (start, end);
	}

	public void SpawnChain(Vector2 start, Vector2 end) {
		Vector2 direction = (end - start) * (((Vector2) chainShaklePrefab.renderer.bounds.size).magnitude / (end - start).magnitude);
		Quaternion rotation = Quaternion.Euler(0, 0, -45 + Mathf.Atan2(direction.y, direction.x)/Mathf.PI*180);

		Vector2 position = start + 0.5f * direction;
		GameObject previousShakle = net;
		while ((position - start).magnitude < (end - start).magnitude) {
			GameObject shakle = (GameObject) Instantiate(chainShaklePrefab, position, rotation);

			previousShakle.GetComponent<HingeJoint2D> ().connectedBody = shakle.rigidbody2D;
			previousShakle = shakle;

			position += direction;
		}

		HingeJoint2D finalJoint = previousShakle.GetComponent<HingeJoint2D> ();
		finalJoint.connectedAnchor = 0.5f * -(Vector2)renderer.bounds.size;
		finalJoint.connectedBody = gameObject.rigidbody2D;
	}
}
