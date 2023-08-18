using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoriesBase
{
	private Dictionary<Type, Repository> repositoriesMap;
	public RepositoriesBase()
	{
		this.repositoriesMap = new Dictionary<Type, Repository>();
	}

	public void CreateAllRepositories()
	{
		this.CreateRepository<BankRepository>();
	}
	private void CreateRepository<T>() where T : Repository, new()
	{
		var repository = new T();
		var type = typeof(T);
		this.repositoriesMap[type] = repository;
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
