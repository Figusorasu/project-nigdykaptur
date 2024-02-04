using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
	PlayerController player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	public void Interact() 
	{
		Debug.Log("Interacted!");
		player.numbersOfCubes++;
	}
}
