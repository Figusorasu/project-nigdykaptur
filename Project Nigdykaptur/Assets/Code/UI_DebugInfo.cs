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

	private GameObject player;

    void Start() {
		player = GameObject.FindGameObjectWithTag("Player");     
    }

    void Update() {
        x_vel_text.text = $"x : {player.GetComponent<NavMeshAgent>().velocity.x}";
        y_vel_text.text = $"y : {player.GetComponent<NavMeshAgent>().velocity.y}";
        z_vel_text.text = $"z : {player.GetComponent<NavMeshAgent>().velocity.z}";
    }
}
