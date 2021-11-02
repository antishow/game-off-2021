using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseTarget : MonoBehaviour
{
	public string targetTag = "Player";
	public float targetCheckFrequency = 0.1f;
	private Transform _target;

	private NavMeshAgent navMeshAgent;

	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		_target = GameObject.FindWithTag(targetTag).transform;
		InvokeRepeating("UpdateTarget", Random.Range(0f, 0.1f), 2f);
	}

	void UpdateTarget()
	{
		navMeshAgent.destination = _target.position;
	}
}
