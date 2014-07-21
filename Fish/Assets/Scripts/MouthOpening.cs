using UnityEngine;
using System.Collections;

public class MouthOpening : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	
	private Sprite closedMouth;
	public Sprite openMouth;

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
		
		if (nearFish.Count == 0) {
			spriteRenderer.sprite = closedMouth;
		}
	}
	
	public void OnTriggerEnter2D(Collider2D collider) {
		Transform other = collider.transform;
		while (other != null && other.tag != "Fish") {
			other = other.transform.parent;
		}
		
		if (other != null && nearFish != null) {
			if (transform.parent.localScale.y > other.transform.localScale.y) {
				if (!nearFish.Contains(other.gameObject)) {
					nearFish.Add(other.gameObject);
					
					if (nearFish.Count == 1) {
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
		while (other != null && other.tag != "Fish") {
			other = other.transform.parent;
		}
		
		if (other != null) {
			if (nearFish.Contains(other.gameObject)) {
				RemoveFish(other.gameObject);
			}
			
			if (ai != null) {
				ai.FishLost(other.gameObject);
			}
		}
	}
}
