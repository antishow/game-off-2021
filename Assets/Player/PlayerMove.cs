using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
	private CharacterController characterController;
	private Vector2 input;
	private Vector3 velocity;

	public float playerSpeed = 5f;

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	void OnMove(InputValue value)
	{
		input = value.Get<Vector2>();
	}

	void Update()
	{
		if (characterController.isGrounded && velocity.y < 0)
		{
			velocity.y = 0f;
		}

		Vector3 move = transform.right * input.x + transform.forward * input.y;
		characterController.Move(move * Time.deltaTime * playerSpeed);
		velocity += Physics.gravity * Time.deltaTime;
		characterController.Move(velocity * Time.deltaTime);
	}
}
