using UnityEngine;
using System.Collections;

//This class destroy all fish within a trigger, when the trigger exit the screen, so the player will die when he get trapped in a net
[RequireComponent(typeof(Collider2D))]
public class NetTrigger : MonoBehaviour {

	private ArrayList trappedFish;	//The fish that are infront of the net

	// Use this for initialization
	public void Start () {
		trappedFish = new ArrayList (10);
	}
	
	public void OnTriggerEnter2D (Collider2D trappedColider) {
		if (trappedColider.gameObject.layer.Equals (LayerMask.NameToLayer("PlayerWall"))) {	
			//Destroy all fish in the net when the net is about to leave the screen
			foreach (GameObject fish in trappedFish) {
				if (fish != null) {
					BloodEffect bloodEffect = fish.GetComponentInChildren<BloodEffect> ();
					if (bloodEffect != null) bloodEffect.Activate();

					Destroy(fish);
				}
			}
		} else {
			//Check if the new object is a fish and add the fish to the list of trapped fish
			GameObject trappedObject = trappedColider.gameObject;
			while (!(trappedObject.tag.Equals("Player") || trappedObject.tag.Equals("Fish")) && trappedObject.transform.parent != null) {
				trappedObject = trappedObject.transform.parent.gameObject;
			}
			if (trappedObject.tag.Equals("Player") || trappedObject.tag.Equals("Fish") && !trappedFish.Contains(trappedObject)) {
				trappedFish.Add(trappedObject);
			}
		}
	}
	
	public void OnTriggerExit2D (Collider2D trappedColider) {
		//Check if the new object is a fish and remove the fish from the list of trapped fish
		GameObject trappedObject = trappedColider.gameObject;
		while (!(trappedObject.tag.Equals("Player") || trappedObject.tag.Equals("Fish")) && trappedObject.transform.parent != null) {
			trappedObject = trappedObject.transform.parent.gameObject;
		}
		if (trappedObject.tag.Equals("Player") || trappedObject.tag.Equals("Fish")) {
			trappedFish.Remove(trappedObject);
		}
	}
}
