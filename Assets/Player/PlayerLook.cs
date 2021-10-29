using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
	private CinemachineFreeLook _freeLook;
	private Vector2 input;

	void Awake()
	{
		_freeLook = GetComponentInChildren<CinemachineFreeLook>();
	}

	void OnLook(InputValue value)
	{
		input = value.Get<Vector2>();
	}

	void Update()
	{
		_freeLook.m_XAxis.Value += input.x * 180 * Time.deltaTime;
		_freeLook.m_YAxis.Value += input.y * -1 * Time.deltaTime;
	}
}
