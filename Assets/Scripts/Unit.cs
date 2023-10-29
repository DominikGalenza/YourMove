using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	[SerializeField] private Animator unitAnimator;

	private Vector3 targetPosition;
	private float stoppingDistance = 0.1f;
	private float moveSpeed = 4f;
	private float rotationSpeed = 10f;

	private void Awake()
	{
		targetPosition = transform.position;
	}

	private void Update()
	{
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
			Vector3 moveDirection = (targetPosition - transform.position).normalized;
			transform.position += moveDirection * moveSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), Time.deltaTime * rotationSpeed);
			unitAnimator.SetBool("IsWalking", true);
		}
		else
		{
			unitAnimator.SetBool("IsWalking", false);
		}
	}
	public void Move(Vector3 targetPosition)
	{
		this.targetPosition = targetPosition;
	}
}
