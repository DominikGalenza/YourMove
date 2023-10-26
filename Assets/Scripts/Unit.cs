using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	private Vector3 targetPosition;
	private float stoppingDistance = 0.1f;
	private float moveSpeed = 4f;

	private void Update()
	{
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
			Vector3 moveDirection = (targetPosition - transform.position).normalized;
			transform.position += moveDirection * moveSpeed * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			Move(new Vector3(4, 0, 4));
		}
	}
	private void Move(Vector3 targetPosition)
	{
		this.targetPosition = targetPosition;
	}
}