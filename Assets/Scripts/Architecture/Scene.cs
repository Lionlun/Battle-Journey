using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene
{
	private InteractorsBase interactorsBase;
	private RepositoriesBase repositoriesBase;
	private SceneConfig sceneConfig;

	public Scene(SceneConfig config)
	{
		this.sceneConfig = config;
		this.interactorsBase = new InteractorsBase(config);
		this.repositoriesBase = new RepositoriesBase(config);
	}

	public Coroutine InitializeAsync()
	{
		return Coroutines.StartRoutine(this.InitializeRoutine());
	}

	private IEnumerator InitializeRoutine()
	{
		interactorsBase.CreateAllInteractors();
		repositoriesBase.CreateAllRepositories();
		yield return null;
		interactorsBase.SendOnCreateToAllInteractors();
		repositoriesBase.SendOnCreateToAllRepositories();
		yield return null;
		interactorsBase.InitializeAllInteractors();
		repositoriesBase.InitializeAllRepositories();
		yield return null;
		interactorsBase.SendOnStartToAllInteractors();
		repositoriesBase.SendOnStartToAllRepositories();
		yield return null;

	}

	public T GetRepository<T>() where T: Repository
	{
		return this.repositoriesBase.GetRepository<T>();
	}

	public T GetInteractor<T>() where T : Interactor
	{
		return this.interactorsBase.GetInteractor<T>();
	}
}
