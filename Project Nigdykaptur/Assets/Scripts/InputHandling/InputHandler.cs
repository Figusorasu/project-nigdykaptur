using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance {get; private set;}
	public InputActions inputAction {get; private set;}

    private void Awake() 
	{
        if(Instance == null) 
		{
            Instance = this;
            DontDestroyOnLoad(Instance);
			inputAction = new InputActions();
        } 
		else 
		{
            Destroy(gameObject);
			return;
		};
	}

	private void OnEnable() 
	{
		inputAction.Enable();
	}
	private void OnDisable() 
	{
		inputAction.Disable();
	}
}

