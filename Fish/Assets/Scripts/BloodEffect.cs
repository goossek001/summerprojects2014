using UnityEngine;
using System.Collections;

public class BloodEffect : MonoBehaviour {

	public void Activate () {
		ParticleSystem particleSystem = GetComponent<ParticleSystem> ();
		particleSystem.emissionRate *= Mathf.Pow(transform.parent.localScale.y, 1.2f);
		particleSystem.Play ();
		transform.parent = null;
		Destroy(gameObject, particleSystem.startLifetime + particleSystem.duration);
	}
}
