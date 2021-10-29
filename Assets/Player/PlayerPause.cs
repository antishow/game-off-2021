using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerPause : MonoBehaviour
{
	PlayerInput playerInput;
	GameController gameController;

	void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		if (!gameController)
		{
			Debug.LogWarning("Player Pause component could not find Game Controller!");
		}

		gameController.OnPause.AddListener(OnGamePaused);
		gameController.OnUnpause.AddListener(OnGameUnpaused);
	}

	void OnPause()
	{
		Debug.Log("Pressed the pause button!");
		gameController.Pause();
	}

	void OnUnpause()
	{
		Debug.Log("Pressed the pause button!");
		gameController.Unpause();
	}

	void OnGamePaused()
	{
		Debug.Log("Game was paused. Switch to Menu Player Input!");
		playerInput.SwitchCurrentActionMap("Menu");
	}

	void OnGameUnpaused()
	{
		Debug.Log("Game was resumed. Switch to Default Player Input!");
		playerInput.SwitchCurrentActionMap("Default");
	}
}
