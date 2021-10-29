using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	private GameObject _menu;
	private GameController _gameController;

	void Awake()
	{
		_menu = gameObject.transform.Find("Panel").gameObject;
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		if (!_gameController)
		{
			Debug.LogWarning("PauseMenu component could not find GameController!");
		}
	}

	void Start()
	{
		_gameController.OnPause.AddListener(Show);
		_gameController.OnUnpause.AddListener(Hide);
	}

	void Show()
	{
		_menu?.SetActive(true);
	}

	void Hide()
	{
		_menu?.SetActive(false);
	}
}
