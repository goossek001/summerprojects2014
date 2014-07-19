﻿using UnityEngine;
using System.Collections;

public class BloodEffectGenerator : MonoBehaviour {

	public void Activate () {
		ParticleSystem particleSystem = GetComponent<ParticleSystem> ();
		particleSystem.emissionRate *= transform.localScale.y;
		particleSystem.Play ();
		transform.parent = null;
		Destroy(gameObject, particleSystem.startLifetime + particleSystem.duration);
	}
}
