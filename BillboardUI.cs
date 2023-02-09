using UnityEngine;

public class BillboardUI : MonoBehaviour {
	Quaternion _originalRotation;

	void Start()
	{
		_originalRotation = transform.rotation;
	}

	void Update()
	{
		transform.rotation = CameraController.Instance.MainCamera.transform.rotation * _originalRotation;   
	}
}