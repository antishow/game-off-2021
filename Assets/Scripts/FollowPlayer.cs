using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class FollowPlayer : MonoBehaviour
{
	private CinemachineVirtualCamera cinemachine;

	void Awake()
	{
		cinemachine = GetComponent<CinemachineVirtualCamera>();
	}

	void Start()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (!player)
		{
			return;
		}

		cinemachine.Follow = player.transform;
		cinemachine.LookAt = player.transform;
	}
}
