using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
	private PlayerInput _playerInput;

	[Header("Movement")]
		[SerializeField] private float lookRotationSpeed = 8f;
		[SerializeField] private LayerMask walkableLayer;
		[SerializeField] private NavMeshAgent _playerAgent;
		[SerializeField] private ParticleSystem clickEffect;
		
	[Header("Interaction System")]
		[SerializeField] private float interactionDistance = 1f;
		[SerializeField] private LayerMask interactableLayer;

		private GameObject interactionTarget = null;

		private IInteractable interactable = null;

		private bool interactionIsPending = false;

		public bool interactableObjectHover = false;
		public bool interactableObjectFound = false;
		public bool interactableFound = false;


	private void Awake() 
	{
		AssignInput();
    }

	private void Start() 
	{
		_playerAgent = GetComponent<NavMeshAgent>();
		
	}

	private void Update() 
	{
		FaceTarget();
		
		if(interactionTarget != null && interactable != null) 
		{
			if(interactionIsPending = true && Vector3.Distance(interactionTarget.transform.position, transform.position) < interactionDistance)
			{	
				interactionIsPending = false;
				interactable.Interact();
				
			}
		}

		interactableObjectFound = interactionTarget != null ? true : false;
		interactableFound = interactable != null ? true : false;

	}

	#region Inputs
		private void AssignInput() 
		{
			_playerInput = new PlayerInput();
			_playerInput.Player.LeftMouseClick.performed += ctx => OnClick();
		}

		private void OnEnable() 
		{
			_playerInput.Enable();
		}

		private void OnDisable() 
		{
			_playerInput.Disable();
		}
	#endregion

	
	private void OnClick() 
	{
		Ray ray = Camera.main.ScreenPointToRay(_playerInput.Player.MousePosition.ReadValue<Vector2>());
		RaycastHit hitInfo;
		if(Physics.Raycast(ray, out hitInfo, 100, walkableLayer)) 
		{
			_playerAgent.destination = hitInfo.point;

			if(clickEffect != null)
			{
				Instantiate(clickEffect, hitInfo.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
			}
		}

		if(Physics.Raycast(ray, out hitInfo, 100, interactableLayer))
		{	
			interactionTarget = hitInfo.transform.gameObject;
			interactable = interactionTarget.GetComponent<IInteractable>();

			if(Vector3.Distance(interactionTarget.transform.position, transform.position) < interactionDistance)
			{
				interactable.Interact();
			}
			else
			{
				interactionIsPending = true;
			}
			
			
		}
		else
		{	
			interactionTarget = null;
		}

	}

	private void FaceTarget() {
		if(_playerAgent.velocity != Vector3.zero)
		{
			Vector3 direction = (_playerAgent.destination - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
		}
	}
	

	private void OnDrawGizmos()
	{
		if(transform.position != _playerAgent.destination)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, _playerAgent.destination);
		}
	}
}