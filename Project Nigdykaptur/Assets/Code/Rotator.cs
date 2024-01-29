using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.2f;

	private void Update()
	{
		transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.Self);
	}
}
