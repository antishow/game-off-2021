using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMove : MonoBehaviour
{
	private CharacterController characterController;
	private NavMeshAgent navMeshAgent;
	private PlayerInput playerInput;
	private Vector2 input;
	private Vector3 velocity;

	public float playerSpeed = 5f;

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	void OnMove(InputValue value)
	{
		input = value.Get<Vector2>();
	}

	void OnPoint(InputValue value)
	{
		Vector2 mousePosition = Mouse.current.position.ReadValue();
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(
			new Vector3(mousePosition.x, mousePosition.y, Camera.main.farClipPlane)
		);

		if (Physics.Raycast(ray, out hit))
		{
			Debug.LogFormat("Clicked on {0} at {1}", hit.transform.gameObject.name, hit.point);
		}
	}

	void Update()
	{
		if (characterController.isGrounded && velocity.y < 0)
		{
			velocity.y = 0f;
		}

		if (input != Vector2.zero)
		{
			Vector3 move = transform.right * input.x + transform.forward * input.y;
			characterController.Move(move * Time.deltaTime * playerSpeed);
		}

		velocity += Physics.gravity * Time.deltaTime;
		characterController.Move(velocity * Time.deltaTime);
	}
}
