using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoriesBase
{
	private Dictionary<Type, Repository> repositoriesMap;
	private SceneConfig sceneConfig;

	public RepositoriesBase(SceneConfig sceneConfig)
	{
		this.sceneConfig = sceneConfig;
	}

	public void CreateAllRepositories()
	{
		this.repositoriesMap = this.sceneConfig.CreateAllRepositories();
	}

	public void SendOnCreateToAllRepositories()
	{
		var allInteractors = this.repositoriesMap.Values;
		foreach (var repository in allInteractors)
		{
			repository.OnCreate();
		}
	}

	public void InitializeAllRepositories()
	{
		var allInteractors = this.repositoriesMap.Values;
		foreach (var repository in allInteractors)
		{
			repository.Initialize();
		}
	}

	public void SendOnStartToAllRepositories()
	{
		var allInteractors = this.repositoriesMap.Values;
		foreach (var repository in allInteractors)
		{
			repository.OnStart();
		}
	}

	public T GetRepository<T>() where T : Repository
	{
		var type = typeof(T);
		return (T)this.repositoriesMap[type];
	}
}
