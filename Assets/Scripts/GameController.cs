using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public const string ROOT = "ROOT";
	public const string NEW_GAME_SCENE = "DevScene";

	private List<string> gameBaseScenes = new List<string> { "Player", "HUD" };

	public bool titleScreenInPlayMode;

	public UnityEvent OnPause;
	public UnityEvent OnUnpause;

	void Start()
	{
		if(!Application.isEditor || titleScreenInPlayMode) {
			StartCoroutine(GoToTitleScreen());
		}
	}

	public void Pause()
	{
		Debug.Log("Paused");
		Time.timeScale = 0;
		OnPause.Invoke();
	}

	public void Unpause()
	{
		Time.timeScale = 1;
		OnUnpause.Invoke();
	}

	private IEnumerator _pruneScenes()
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(ROOT));

		List<AsyncOperation> unloads = new List<AsyncOperation>();
		for (int i=0; i<SceneManager.sceneCount; i++)
		{
			Scene s = SceneManager.GetSceneAt(i);
			if (s.name != ROOT)
			{
				unloads.Add(SceneManager.UnloadSceneAsync(s));
			}
		}

		yield return new WaitUntil(() => unloads.TrueForAll(u => u.isDone));
	}

	public IEnumerator GoToTitleScreen()
	{
		StartCoroutine(_pruneScenes());
		yield return SceneManager.LoadSceneAsync("TitleScreen", LoadSceneMode.Additive);
	}

	public void GoToGameScene(string scene)
	{
		StartCoroutine(_loadGameScene(scene));
	}

	private IEnumerator _loadGameScene(string scene)
	{
		StartCoroutine(_pruneScenes());

		List<AsyncOperation> loading = gameBaseScenes
		 .Select(s => SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive))
		 .ToList<AsyncOperation>();

		loading.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));

		yield return new WaitUntil(() => loading.TrueForAll(u => u.isDone));

		SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
	}
}
