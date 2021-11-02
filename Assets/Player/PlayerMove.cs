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

	void OnDrawGizmos()
	{
		if (navMeshAgent)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(navMeshAgent.destination, 0.5f);
		}
	}

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.updatePosition = false;
		navMeshAgent.updateRotation = false;
	}

	void OnMove(InputValue value)
	{
		input = value.Get<Vector2>();
		navMeshAgent.enabled = false;
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
			navMeshAgent.enabled = true;
			navMeshAgent.destination = hit.point;
		}
	}

	void Update()
	{
		Vector3 move = Vector3.zero;

		if (navMeshAgent.enabled && navMeshAgent.remainingDistance >= 0.01)
		{
			move = navMeshAgent.nextPosition - transform.position;
		}
		else if (input != Vector2.zero)
		{
			float frameSpeed = Time.deltaTime * playerSpeed;
			move = frameSpeed * (transform.right * input.x + transform.forward * input.y);
			NavMeshHit hit;
			if (NavMesh.SamplePosition(transform.position + move, out hit, frameSpeed * 2, NavMesh.AllAreas))
			{
				move = hit.position - transform.position;
			}
		}

		characterController.Move(move);
	}
}
