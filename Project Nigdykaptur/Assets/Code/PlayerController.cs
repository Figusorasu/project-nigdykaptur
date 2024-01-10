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
	#region Variables & Components
		[Header("Movement")]
			public float lookRotationSpeed = 8f;	

			[SerializeField] private LayerMask whatCanBeClickedOn;
			[SerializeField] private NavMeshAgent _playerAgent;
			[SerializeField] private ParticleSystem clickEffect;

			private PlayerInput _playerInput;

	#endregion

	private void Awake() {
		AssignInput();
    }

	private void Start() {
		_playerAgent = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		FaceTarget();
	}

	#region Inputs
		private void AssignInput() {
			_playerInput = new PlayerInput();
			_playerInput.Player.Move.performed += ctx => Move();
		}

		private void OnEnable() {
			_playerInput.Enable();
		}

		private void OnDisable() {
			_playerInput.Disable();
		}
	#endregion

	#region Movement
		private void Move() {
			Ray mouseTargetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if(Physics.Raycast(mouseTargetRay, out hitInfo, 100, whatCanBeClickedOn)) {
				_playerAgent.destination = hitInfo.point;
				if(clickEffect != null) {
					Instantiate(clickEffect, hitInfo.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
				}
			}
		}

		private void FaceTarget() {
			if(_playerAgent.velocity != Vector3.zero) {
				Vector3 direction = (_playerAgent.destination - transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
			}
		}
	#endregion





}