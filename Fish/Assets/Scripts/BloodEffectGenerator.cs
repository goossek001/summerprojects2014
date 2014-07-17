using UnityEngine;
using System.Collections;

public class BloodEffectGenerator : MonoBehaviour {

	public void Activate () {
		Debug.Log ("activate");
		ParticleSystem particleSystem = GetComponent<ParticleSystem> ();
		particleSystem.Play ();
		transform.parent = null;
		Destroy(gameObject, particleSystem.startLifetime);
	}
}
