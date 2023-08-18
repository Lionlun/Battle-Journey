using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneManagerBase
{
	public event Action<Scene> OnSceneLoadedEvent;
	public Scene Scene { get; private set; }
	public bool IsLoading { get; private set; }

	protected Dictionary <string, SceneConfig> sceneConfigMap;

	public SceneManagerBase()
	{
		this.sceneConfigMap = new Dictionary<string, SceneConfig>();
		this.InitScenesMap();
	}

	public abstract void InitScenesMap();

	public Coroutine LoadCurrentSceneAsync()
	{
		if (this.IsLoading)
		{
			throw new Exception("Scene is loading now");
		}
		var sceneName = SceneManager.GetActiveScene().name;
		var config = this.sceneConfigMap[sceneName];
		return Coroutines.StartRoutine(this.LoadCurrentSceneRoutine(config));
	}

	private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig)
	{
		this.IsLoading = true;

		yield return Coroutines.StartRoutine(this.InitializeSceneRoutine(sceneConfig));

		this.IsLoading = false;
		this.OnSceneLoadedEvent?.Invoke(this.Scene);
	}

	public Coroutine LoadNewSceneAsync(string sceneName)
	{
		if (this.IsLoading)
		{
			throw new Exception("Scene is loading now");
		}

		var config = this.sceneConfigMap[sceneName];
		return Coroutines.StartRoutine(this.LoadNewSceneRoutine(config));
	}

	public T GetRepository<T>() where T : Repository
	{
		return this.Scene.GetRepository<T>();
	}

	public T GetInteractor<T>() where T : Interactor
	{
		return this.Scene.GetInteractor<T>();
	}

	private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
	{
		this.IsLoading = true;

		yield return Coroutines.StartRoutine(this.LoadSceneRoutine(sceneConfig));
		yield return Coroutines.StartRoutine(this.InitializeSceneRoutine(sceneConfig));

		this.IsLoading = false;
		this.OnSceneLoadedEvent?.Invoke(this.Scene);
	}
	private IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
	{
		var async = SceneManager.LoadSceneAsync(sceneConfig.sceneName);
		async.allowSceneActivation = false;
		while (async.progress < 0.9f)
		{
			yield return null;
		}
		async.allowSceneActivation = true;

	}

	private IEnumerator InitializeSceneRoutine(SceneConfig sceneConfig)
	{
		this.Scene = new Scene(sceneConfig);
		yield return this.Scene.InitializeAsync();
	}


}
