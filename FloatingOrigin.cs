using System.Collections.Generic;
using UnityEngine;

public class FloatingOrigin : MonoBehaviour
{
	public float Threshold = 100.0f;
	[Tooltip("Set 0 to disable")]
	public float PhysicsThreshold = 1000.0f; 

	public float DefaultSleepThreshold = 0.14f;

	ParticleSystem.Particle[] _parts = null;

	GameObject _mainCamera;

	Object[] _objects;

	LevelGenerator _levelGenerator;

	void Awake() {
		_mainCamera = Camera.main.gameObject;
		_levelGenerator = FindObjectOfType<LevelGenerator>();
		
		Object[] allObjects = FindObjectsOfType(typeof(Transform));
		List<Object> rootObjects = new List<Object>();

		foreach (Object o in allObjects) {
			Transform t = (Transform) o;
			if (t.parent == null &&
				!t.CompareTag("Level")) {
				rootObjects.Add(o);
			}
		}

		_objects = rootObjects.ToArray();
	}

	void LateUpdate() {
		Vector3 cameraPosition = _mainCamera.transform.position;

		if (cameraPosition.magnitude > Threshold) {
			foreach (Object o in _objects) {
				Transform t = (Transform) o;
				t.position -= cameraPosition;
			}

			foreach (GameObject section in _levelGenerator.GetSections()) {
				section.transform.position -= cameraPosition;
			}

			Object[] ps = FindObjectsOfType(typeof(ParticleSystem));
			foreach (Object o in ps) {
				ParticleSystem sys = (ParticleSystem) o;

				if (sys.simulationSpace != ParticleSystemSimulationSpace.World)
					continue;

				int particlesNeeded = sys.maxParticles;

				if (particlesNeeded <= 0)
					continue;

				bool wasPaused = sys.isPaused;
				bool wasPlaying = sys.isPlaying;

				if (!wasPaused)
					sys.Pause();

				if (_parts == null || _parts.Length < particlesNeeded) {
					_parts = new ParticleSystem.Particle[particlesNeeded];
				}

				int num = sys.GetParticles(_parts);

				for (int i = 0; i < num; i++) {
					_parts[i].position -= cameraPosition;
				}

				sys.SetParticles(_parts, num);

				if (wasPlaying)
					sys.Play();
			}

			if (PhysicsThreshold > 0f) {
				float physicsThreshold2 = PhysicsThreshold * PhysicsThreshold; // simplify check on threshold
				Object[] rBodys = FindObjectsOfType(typeof(Rigidbody));
				foreach (Object o in rBodys) {
					Rigidbody r = (Rigidbody) o;
					if (r.gameObject.transform.position.sqrMagnitude > physicsThreshold2)
						r.sleepThreshold = float.MaxValue;
					else
						r.sleepThreshold = DefaultSleepThreshold;
				}
			}
		}
	}
}