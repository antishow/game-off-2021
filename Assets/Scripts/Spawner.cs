using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject Prefab;
	public float SpawnFrequency = .3f;
	public int MaxChildren = 32;

	private Transform _children;

	void Awake()
	{
		_children = transform.Find("Spawn");
	}

	void Start()
	{
		InvokeRepeating("Spawn", SpawnFrequency, SpawnFrequency);
	}

	void Spawn()
	{
		if (_children.childCount >= MaxChildren)
		{
			return;
		}

		GameObject child = GameObject.Instantiate(Prefab, transform.position, transform.rotation);
		child.transform.parent = _children;
	}
}
