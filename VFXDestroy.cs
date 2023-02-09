using System;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class VFXDestroy : MonoBehaviour {
	public bool TimeBased;
	public float TTL = 20f;
	VisualEffect _vfx;
	bool _hasStarted;
	float _lifetime;
	
	void Awake() {
		_vfx = GetComponent<VisualEffect>();
	}

	void Update() {
		if (TimeBased) {
			_lifetime += Time.deltaTime;
			if(_lifetime > TTL)
				Destroy(gameObject);
			return;
		}
		if (_vfx.aliveParticleCount > 0)
			_hasStarted = true;
		
		if (_hasStarted && _vfx.aliveParticleCount == 0) {
			Destroy(gameObject);
		}
	}
}