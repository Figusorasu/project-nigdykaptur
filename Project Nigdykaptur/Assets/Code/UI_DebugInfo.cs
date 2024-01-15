using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class UI_DebugInfo : MonoBehaviour
{	
	[SerializeField] private TMP_Text x_vel_text;
	[SerializeField] private TMP_Text y_vel_text;
	[SerializeField] private TMP_Text z_vel_text;

	[SerializeField] private TMP_Text interactionFoundTXT;
	[SerializeField] private TMP_Text interactableFoundTXT;

	private GameObject player;

    void Start() {
		player = GameObject.FindGameObjectWithTag("Player");     
    }

    void Update() {

		
        x_vel_text.text = $"x : {Mathf.Round(player.GetComponent<NavMeshAgent>().velocity.x)}";
        y_vel_text.text = $"y : {Mathf.Round(player.GetComponent<NavMeshAgent>().velocity.y)}";
        z_vel_text.text = $"z : {Mathf.Round(player.GetComponent<NavMeshAgent>().velocity.z)}";

		interactionFoundTXT.text = $"Interactable Object Found: {player.GetComponent<PlayerController>().interactableObjectFound}";
		interactableFoundTXT.text = $"Interactable Found: {player.GetComponent<PlayerController>().interactableFound}";
    }
}
