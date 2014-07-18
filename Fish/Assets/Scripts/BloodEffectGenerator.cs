using UnityEngine;
using System.Collections;

public class BloodEffectGenerator : MonoBehaviour {

	public void Activate () {
		ParticleSystem particleSystem = GetComponent<ParticleSystem> ();
		particleSystem.Play ();
		transform.parent = null;
		Destroy(gameObject, particleSystem.startLifetime);
	}
}
