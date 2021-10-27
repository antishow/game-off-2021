using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUI : MonoBehaviour
{
	GameController gameController;

	void Awake()
	{
		GameObject _gameController = GameObject.FindGameObjectWithTag("GameController");
		if (!_gameController)
		{
			Debug.LogWarning("Title Screen could not find the GameController");
			return;
		}

		gameController = _gameController.GetComponent<GameController>();
	}

	public void NewGame()
	{
		gameController.GoToGameScene(GameController.NEW_GAME_SCENE);
	}
}
