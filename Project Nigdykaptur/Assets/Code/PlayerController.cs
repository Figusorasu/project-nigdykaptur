using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	public LayerMask whatCanBeClickedOn;

	[HideInInspector] public NavMeshAgent playerAgent;

	[SerializeField] private GameObject targetIndicator;

	private Ray mouseTargetRay;
	private RaycastHit hitInfo;
	private bool canMove = false;

    void Start() {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
		mouseTargetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(mouseTargetRay, out hitInfo, 100, whatCanBeClickedOn)) {
			canMove = true;
		} else canMove = false;

        if(Input.GetMouseButton(0) && canMove) {
			
			Move();
		}
    }

	public async void Move() {
		playerAgent.SetDestination(hitInfo.point);


		await Task.Delay(0);

	}
}
