using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler inputHandler;

	[SerializeField] private InputActionAsset actionAsset;

	public UnityEvent ClickToMoveEvent;// {get; private set; }


    private void Awake() 
	{
        if(inputHandler == null) 
		{
            inputHandler = this;
			AssignInputActions();
            DontDestroyOnLoad(inputHandler);
        } 
		else 
		{
            Destroy(gameObject);
			return;
		};
	}

	private void AssignInputActions()
	{
		
	}


	private void OnEnable() 
	{
		actionAsset.Enable();
	}
	private void OnDisable() 
	{
		actionAsset.Disable();
	}
}

