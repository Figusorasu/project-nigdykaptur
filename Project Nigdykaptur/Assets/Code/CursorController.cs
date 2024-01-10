using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
	public static CursorController instance {get; private set;}	

    [SerializeField] private Texture2D cursorTextureDefault;
    [SerializeField] private Texture2D cursorTextureInteract;
    [SerializeField] private Texture2D cursorTextureCanWalkOn;
    [SerializeField] private Texture2D cursorTextureCantWalkOn;

	[SerializeField] private Vector2 clickPosition = Vector2.zero;

	private void Awake() {
		if(instance == null) {
			instance = this;
		}
	}

	private void Start() {
		Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
	}

	public void SetToMode(ModeOfCursor modeOfCursor) {
		switch(modeOfCursor) {
			case ModeOfCursor.Default:
				Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
				break;
			case ModeOfCursor.Interact:
				Cursor.SetCursor(cursorTextureInteract, clickPosition, CursorMode.Auto);
				break;
			case ModeOfCursor.CanWalkOn:
				Cursor.SetCursor(cursorTextureCanWalkOn, clickPosition, CursorMode.Auto);
				break;
			case ModeOfCursor.CantWalkon:
				Cursor.SetCursor(cursorTextureCantWalkOn, clickPosition, CursorMode.Auto);
				break;
			default:
				break;
		}
	}
}

public enum ModeOfCursor {
	Default,
	Interact,
	CanWalkOn,
	CantWalkon
}
