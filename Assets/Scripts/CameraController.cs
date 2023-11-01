using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float rotationSpeed = 100f;

	private void Update()
	{
		MoveCamera();
		RotateCamera();
	}

	private void MoveCamera()
	{
		Vector3 inputMoveDirection = new Vector3(0, 0, 0);

		if (Input.GetKey(KeyCode.W))
		{
			inputMoveDirection.z = +1f;
		}
		if (Input.GetKey(KeyCode.S))
		{
			inputMoveDirection.z = -1f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			inputMoveDirection.x = -1f;
		}
		if (Input.GetKey(KeyCode.D))
		{
			inputMoveDirection.x = +1f;
		}

		Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
		transform.position += moveVector * moveSpeed * Time.deltaTime;
	}

	private void RotateCamera()
	{
		Vector3 rotationVector = new Vector3(0, 0, 0);

		if (Input.GetKey(KeyCode.Q))
		{
			rotationVector.y = +1f;
		}
		if (Input.GetKey(KeyCode.E))
		{
			rotationVector.y = -1f;
		}

		transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
	}
}
