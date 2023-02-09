using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour {
	public UnityEvent<Collision> OnCollideEnter;
	public UnityEvent<Collision> OnCollideStay;
	public UnityEvent<Collision> OnCollideExit;
	public UnityEvent<Collider> OnTrigEnter;
	public UnityEvent<Collider> OnTrigStay;
	public UnityEvent<Collider> OnTrigExit;

	void OnCollisionEnter(Collision collision) {
		OnCollideEnter?.Invoke(collision);
	}

	void OnCollisionStay(Collision collisionInfo) {
		OnCollideStay?.Invoke(collisionInfo);
	}

	void OnCollisionExit(Collision other) {
		OnCollideExit?.Invoke(other);
	}

	void OnTriggerEnter(Collider other) {
		OnTrigEnter?.Invoke(other);
	}

	void OnTriggerStay(Collider other) {
		OnTrigStay?.Invoke(other);
	}

	void OnTriggerExit(Collider other) {
		OnTrigExit?.Invoke(other);
	}
}