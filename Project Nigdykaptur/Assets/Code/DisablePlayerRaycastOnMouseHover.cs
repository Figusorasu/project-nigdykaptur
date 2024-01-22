using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisablePlayerRaycastOnMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private PlayerController _player => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

	public void OnPointerEnter(PointerEventData eventData)
	{
		_player.disablePlayerRaycast = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_player.disablePlayerRaycast = false;
	}
}
