using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickableRotation : MonoBehaviour
{
	public float rotationSpeed = 100.0f;

	void Update ()
	{
		transform.Rotate(Vector3.up * Time.deltaTime * this.rotationSpeed);
	}

	public void SetRotationSpeed(float speed)
	{
		this.rotationSpeed = speed;
	}
}
