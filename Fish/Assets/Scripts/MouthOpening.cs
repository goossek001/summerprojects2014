using UnityEngine;
using System.Collections;

public class MouthOpening : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	
	private Sprite closedMouth;
	public Sprite openMouth;

	private int fishCount;

	// Use this for initialization
	public void Start () {
		spriteRenderer = transform.parent.GetComponent<SpriteRenderer> ();
		closedMouth = spriteRenderer.sprite;

		fishCount = 0;
	}

	public void CloseMouth() {
		fishCount = 0;
		spriteRenderer.sprite = closedMouth;
	}
	
	public void OnTriggerEnter2D(Collider2D collider) {
		Transform other = collider.transform;
		while (other != null && other.tag != "Fish") {
			other = other.transform.parent;
		}

		if (other != null && other.transform.localScale.x <= transform.localScale.x) {
			fishCount++;

			if (fishCount == 1) {
				spriteRenderer.sprite = openMouth;
			}
		}
	}
	
	public void OnTriggerExit2D(Collider2D collider) {
		Transform other = collider.transform;
		while (other != null && other.tag != "Fish") {
			other = other.transform.parent;
		}
		
		if (other != null && other.transform.localScale.x <= transform.localScale.x && fishCount > 0) {
			fishCount--;
			
			if (fishCount == 0) {
				spriteRenderer.sprite = closedMouth;
			}
		}
	}
}
