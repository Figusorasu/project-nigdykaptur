using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IKFootSolver : MonoBehaviour
{
	private Transform body;
	private float lerp;

	[SerializeField] private float stepDistance = 1f;
	[SerializeField] private float stepHeight = 0.5f;
	[SerializeField] private float speed = 3f;

	private Vector3 currentPosition;
	private Vector3 newPosition;
	private Vector3 oldPosition;

	[SerializeField] private float footSpacing;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private GameObject oppositeLeg;

	private void Start()
	{
		body = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		currentPosition = transform.position;

	}

	private void Update()
    {	
		if(oppositeLeg.GetComponent<Transform>().position == oppositeLeg.GetComponent<IKFootSolver>().currentPosition) {
			transform.position = currentPosition;
		}

			Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
			if(Physics.Raycast(ray, out RaycastHit info, 10, groundMask)) {
				if(Vector3.Distance(newPosition, info.point) > stepDistance) {
					lerp = 0;
					newPosition = info.point;
				}
			}
		
			if (lerp < 1) {
				Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
				footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

				currentPosition = footPosition;
				lerp += speed * Time.deltaTime;
			}
			else {
				oldPosition = newPosition;
			}
		
		
		

		/*
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
		if(Physics.Raycast(ray, out RaycastHit info, 10, groundMask)) {
			transform.position = info.point;
		}*/
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(newPosition, 0.1f);
	}
}
