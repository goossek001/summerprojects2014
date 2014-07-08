using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	
	public float radius = 10f;
	
	public bool IsClear() {
		Collider[] cols = Physics.OverlapSphere(transform.position, radius);
		foreach(Collider c in cols) {
			if(c.tag=="Player") {
				return false;
			}
		}
		return true;
	}
	
}
