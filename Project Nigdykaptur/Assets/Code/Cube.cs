using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

	public void Interact() {
		Debug.Log("Interacted!");
	}
}
