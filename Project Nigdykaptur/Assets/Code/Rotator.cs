using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
	[SerializeField] private Vector3 rotationVector;

	private void Update()
	{
		transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime, Space.Self);
	}
}
