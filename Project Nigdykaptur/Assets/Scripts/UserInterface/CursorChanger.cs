using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{	
	[SerializeField] private ModeOfCursor cursorMode;


    public void OnPointerEnter(PointerEventData eventData) {
		CursorController.Instance.SetToMode(cursorMode);
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		CursorController.Instance.SetToMode(ModeOfCursor.Default);
	}
    
}
