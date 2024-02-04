using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSADCameraControler : MonoBehaviour
{
	private Transform cam => gameObject.GetComponent<Transform>();
	[SerializeField] float speed = 0.2f;
	[SerializeField] float rotationSpeed = 0.2f;

    void Update()
    {
		if(Input.GetKey(KeyCode.W))
		{
			cam.Translate(Vector3.forward * speed, Space.Self);
		}
		if(Input.GetKey(KeyCode.D))
		{
			cam.Translate(Vector3.right * speed, Space.Self);
		}
		if(Input.GetKey(KeyCode.A))
		{
			cam.Translate(Vector3.left * speed, Space.Self);
		}
		if(Input.GetKey(KeyCode.S))
		{
			cam.Translate(Vector3.back * speed, Space.Self);
		}

		if(Input.GetKey(KeyCode.E))
		{
			cam.eulerAngles += new Vector3(0, 1, 0) * rotationSpeed;
		}
		if(Input.GetKey(KeyCode.Q))
		{
			cam.eulerAngles += new Vector3(0, -1, 0) * rotationSpeed;
		}

		if(Input.GetKey(KeyCode.R))
		{
			cam.eulerAngles += new Vector3(-1, 0, 0) * rotationSpeed;
		} 
		if(Input.GetKey(KeyCode.F))
		{
			cam.eulerAngles += new Vector3(1, 0, 0) * rotationSpeed;
		}

		if(Input.GetKey(KeyCode.T))
		{
			cam.Translate(Vector3.up * speed / 2, Space.Self);
		} 
		if(Input.GetKey(KeyCode.G))
		{
			cam.Translate(Vector3.down * speed / 2, Space.Self);
		}

    }
}
