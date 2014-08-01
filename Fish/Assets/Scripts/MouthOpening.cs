using UnityEngine;
using System.Collections;

public class MouthOpening : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	
	private Sprite closedMouth;
	public Sprite openMouth;
	public Sprite scaredFace;

	private FishAI ai;

	private ArrayList nearFish;

	// Use this for initialization
	public void Start () {
		spriteRenderer = transform.parent.GetComponent<SpriteRenderer> ();
		closedMouth = spriteRenderer.sprite;

		ai = (gameObject.layer == LayerMask.NameToLayer ("Player") ? null : transform.parent.GetComponent<FishAI> ());

		nearFish = new ArrayList (6);
	}

	private void RemoveFish(GameObject fish) {
		nearFish.Remove (fish);
		nearFish.Remove (null); 	//!!!Bad bug fix, somethimes OnTriggerExit doesnt call before fish destruction. This code will remove those fish.

		if (nearFish.Count == 0 && spriteRenderer.sprite != scaredFace) {
			spriteRenderer.sprite = closedMouth;
		}
	}

	public void Scare() {
		spriteRenderer.sprite = scaredFace;
	}
	
	public void Relax() {
		spriteRenderer.sprite = (nearFish.Count == 0? closedMouth: openMouth);
	}
	
	public void OnTriggerEnter2D(Collider2D collider) {
		Transform other = collider.transform;
		while (other != null && (other.tag != "Fish" && other.tag != "Player")) {
			other = other.transform.parent;
		}
	
		if (other != null && nearFish != null && (transform.parent.tag == "Player" || other.tag == "Player")) {
			if (transform.parent.localScale.y > other.transform.localScale.y) {
				if (!nearFish.Contains(other.gameObject)) {
					nearFish.Add(other.gameObject);
					
					if (nearFish.Count == 1 && spriteRenderer.sprite != scaredFace) {
						spriteRenderer.sprite = openMouth;
					}
				}
			}
		
			if (ai != null) {
				ai.FishLocated(other.gameObject);
			}
		}
	}
	
	public void OnTriggerExit2D(Collider2D collider) {
		Transform other = collider.transform;
		while (other != null &&  (other.tag != "Fish" && other.tag != "Player")) {
			other = other.transform.parent;
		}
		
		if (other != null && (transform.parent.tag == "Player" || other.tag == "Player")) {
			if (nearFish.Contains(other.gameObject)) {
				RemoveFish(other.gameObject);
			}
			
			if (ai != null) {
				ai.FishLost(other.gameObject);
			}
		}
	}
}
