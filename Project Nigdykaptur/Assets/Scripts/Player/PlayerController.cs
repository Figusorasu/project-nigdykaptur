using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;
using UnityEditor.PackageManager;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	//private InputActions _playerInput;

	[Header("Movement")]
		[SerializeField] private float lookRotationSpeed = 8f;
		//[SerializeField] private LayerMask walkableLayer;
		private int walkableLayerMask;

		[SerializeField] private NavMeshAgent _playerAgent;
		[SerializeField] private ParticleSystem clickEffect;

		private LayerMask clickableLayers;

		public bool disablePlayerRaycast = false;
		
	[Header("Interaction System")]
		[SerializeField] private float interactionDistance = 1f;
		[SerializeField] private Canvas promptCanvas;

		private int interactableLayerMask;

		private GameObject interactionTarget = null;
		private IInteractable interactable = null;

		private bool interactionIsPending = false;

	[Header("Debuging")]
		public bool interactableObjectFound = false;
		public bool interactableFound = false;
		public int numbersOfCubes = 0;


	#region Unity Methods
		private void Awake() 
		{
			AssignInput();
		
			clickableLayers = LayerMask.GetMask("Ground", "Interactable");
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
				InteractWithObject();
			}

			//Debuging
			interactableObjectFound = interactionTarget != null ? true : false;
			interactableFound = interactable != null ? true : false;
			Debug.Log($"Disable Player Raycast{disablePlayerRaycast}");
			//
		}
	#endregion

	#region Inputs
		private void AssignInput() 
		{
			//_playerInput = new InputActions();
			//_playerInput.Player.LeftMouseClick.performed += ctx => OnClick();
			
			InputHandler.Instance.inputAction.Player.LeftMouseClick.performed += ctx => OnClick();
		}

		//private void OnEnable() {_playerInput.Enable();}
		//private void OnDisable() {_playerInput.Disable();}
	
		private void OnClick() 
		{
			if(!disablePlayerRaycast) 
			{
				Ray ray = Camera.main.ScreenPointToRay(InputHandler.Instance.inputAction.Player.MousePosition.ReadValue<Vector2>());
				RaycastHit hitInfo;

				walkableLayerMask = LayerMask.NameToLayer("Ground");
				interactableLayerMask = LayerMask.NameToLayer("Interactable");

				if(Physics.Raycast(ray, out hitInfo, 100)) 
				{
					LayerMask clickedLayer = hitInfo.collider.gameObject.layer;
					ClearInteractableObjects();

					if(clickedLayer.value == walkableLayerMask)
					{
						Move(hitInfo);
					}
			
					if(clickedLayer.value == interactableLayerMask)
					{	
						SetInteraction(hitInfo);
					}
				}	
			}
		}
	#endregion

	#region Movement
		void Move(RaycastHit hitInfo)
		{
			_playerAgent.destination = hitInfo.point;
			if(clickEffect != null)
			{
				Instantiate(clickEffect, hitInfo.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
			}
		}
	
		void FaceTarget()
		{
			if(_playerAgent.velocity != Vector3.zero)
			{
				Vector3 direction = (_playerAgent.destination - transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
			}
		}
	#endregion

	#region Interaction System
		void SetInteraction(RaycastHit hitInfo)
		{
			interactionTarget = hitInfo.transform.gameObject;
			interactable = interactionTarget.GetComponent<IInteractable>();

			if(!InteractionTargetIsInRange())
			{
				_playerAgent.destination = hitInfo.point;
			}

			if(clickEffect != null)
			{
				var particlePos = interactionTarget.transform.position;
				Instantiate(clickEffect, particlePos, clickEffect.transform.rotation);
			}
			interactionIsPending = true;
		}

		void InteractWithObject()
		{
			if(interactionIsPending && InteractionTargetIsInRange())
			{	
				interactable.Interact();
				promptCanvas.GetComponent<Prompt>().promptText.text = "Interacted with object";
				Instantiate(promptCanvas);
				interactionTarget = null;
				interactable = null;
			}
		}

		void ClearInteractableObjects()
		{
			interactionIsPending = false;
			interactionTarget = null;
			interactable = null;
		}

		bool InteractionTargetIsInRange()
		{
			return Vector3.Distance(interactionTarget.transform.position, transform.position) < interactionDistance;
		}
	#endregion

	private void OnDrawGizmos()
	{
		if(transform.position != _playerAgent.destination)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, _playerAgent.destination);
		}
	}
}